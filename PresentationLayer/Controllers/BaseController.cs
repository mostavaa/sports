using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using PresentationLayer.Models;
using PresentationLayer.Models.Repositories;

namespace PresentationLayer.Controllers
{
    public class BaseController : Controller
    {
        public readonly UnitOfWork Context = new UnitOfWork();
        public ContentResult ToJson(object Result)
        {
            var serializer = new JavaScriptSerializer()
            {
                MaxJsonLength = int.MaxValue
            };
            var result = new ContentResult
            {
                Content = serializer.Serialize(Result),
                ContentType = "application/json"
            };
            return result;
        }

        public IEnumerable<SelectListItem> AllMatches(long selectedMatchId = 0, bool addNullEntry = true, long? ChampionshipId = null)
        {
            UnitOfWork uow = new UnitOfWork();
            List<SelectListItem> result = addNullEntry ? new List<SelectListItem>()
            {
                new SelectListItem()
                {
                    Selected = true
                }
            } : new List<SelectListItem>();

            result.AddRange(uow.MatchRepository.Get().Where(c => c.ChampionshipId == ChampionshipId).Select(o => new SelectListItem()
            {
                Value = o.Id.ToString(),
                Text = o.FirstTeamName + @"-" + o.SecondTeamName,
                Selected = o.Id == selectedMatchId,

            }));
            return result;
        }
        public ContentResult ChampionshipMatches(long id)
        {
            UnitOfWork uow = new UnitOfWork();
            return ToJson(uow.MatchRepository.Get().Where(o => o.ChampionshipId == id).Select(o => new SelectListItem()
            {
                Value = o.Id.ToString(),
                Text = o.FirstTeamName + @"-" + o.SecondTeamName,

            }).ToList());
        }
        public IEnumerable<SelectListItem> AllChampionships(long selectedChampionshipId = 0, bool addNullEntry = true)
        {
            UnitOfWork uow = new UnitOfWork();
            List<SelectListItem> result = addNullEntry ? new List<SelectListItem>()
            {
                new SelectListItem()
                {
                    Selected = true
                }
            } : new List<SelectListItem>();
            result.AddRange(uow.ChampionshipRepository.Get().Select(o => new SelectListItem()
            {
                Value = o.Id.ToString(),
                Text = o.ChampName,
                Selected = o.Id == selectedChampionshipId,

            }));
            return result;
        }
        public IEnumerable<SelectListItem> AllAlbums(long selectedAlbumId = 0, bool addNullEntry = true)
        {
            UnitOfWork uow = new UnitOfWork();
            List<SelectListItem> result = addNullEntry ? new List<SelectListItem>()
            {
                new SelectListItem()
                {
                    Selected = true
                }
            } : new List<SelectListItem>();
            result.AddRange(uow.AlbumRepository.Get().Select(o => new SelectListItem()
            {
                Value = o.Id.ToString(),
                Text = o.AlbumName,
                Selected = o.Id == selectedAlbumId,
            }));
            return result;
        }
        public IEnumerable<SelectListItem> AllNews(long selectedNewsId = 0, bool addNullEntry = true)
        {
            UnitOfWork uow = new UnitOfWork();
            List<SelectListItem> result = addNullEntry ? new List<SelectListItem>()
            {
                new SelectListItem()
                {
                    Selected = true
                }
            } : new List<SelectListItem>();
            result.AddRange(uow.NewsRepository.Get().Select(o => new SelectListItem()
            {
                Value = o.Id.ToString(),
                Text = o.NewsHeading,
                Selected = o.Id == selectedNewsId,
            }));
            return result;
        }
        #region Upload Image
        public byte[] ConvertPhotoToBytes(string photo)
        {
            byte[] logo = null;
            if (photo != null)
            {
                logo = ConvertStringTOBytesArray(photo);
            }
            return logo;
        }


        private byte[] ConvertStringTOBytesArray(string str)
        {
            byte[] bytesArray = null;
            try
            {
                try
                {
                    bytesArray = Encoding.ASCII.GetBytes(str);

                }
                catch (Exception)
                {
                    str = "";
                }
                return bytesArray;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public void UploadImage(Guid id)
        {
            var httpPostedFileBase = Request.Files[0];
            if (httpPostedFileBase != null && httpPostedFileBase.ContentLength>0)
                using (var binaryReader = new BinaryReader(httpPostedFileBase.InputStream))
                {
                    UploadImagesToServer(id, binaryReader.ReadBytes(httpPostedFileBase.ContentLength));
                }
        }
        public string UploadImagesToServer(Guid id, byte[] imageBytes)
        {
            string imageName = string.Empty;
            try
            {
                imageName = id + ".png";

                string saveImagePath = Server.MapPath("~/Content/Images/") + imageName;
                string saveIconImagePath = Server.MapPath("~/Content/Images/Icons/") + imageName;

                if (imageBytes != null) System.IO.File.WriteAllBytes(saveImagePath, imageBytes);
                byte[] iconBytes = ImageToByte(ResizeImage(ByteArrayToImage(imageBytes), Common.IconWidth, Common.IconHeight));
                if (iconBytes != null) System.IO.File.WriteAllBytes(saveIconImagePath, iconBytes);
            }
            catch
            {
                // ignored
            }
            return imageName;
        }
        public static byte[] ImageToByte(Image img)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }
        private Image ByteArrayToImage(byte[] byteArrayIn)
        {

            using (var ms = new MemoryStream(byteArrayIn))
            {
                return Image.FromStream(ms);
            }
        }
        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }
        private byte[] Convertbase64ToBytesArray(string stringBase64)
        {
            try
            {
                var bytesArray = Convert.FromBase64String(stringBase64);
                return bytesArray;
            }
            catch (Exception)
            {
                return null;
            }

        }
        #endregion


        protected override void Dispose(bool disposing)
        {
            Context.Dispose();
            base.Dispose(disposing);

        }

        /// <summary>
        /// before Executing action
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
        }
    }
}