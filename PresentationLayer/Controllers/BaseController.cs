using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
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

        public IEnumerable<SelectListItem> AllMatches(long selectedMatchId = 0, bool addNullEntry = true,long? ChampionshipId=null)
        {
            UnitOfWork uow = new UnitOfWork();
            List<SelectListItem> result = addNullEntry ? new List<SelectListItem>()
            {
                new SelectListItem()
                {
                    Selected = true
                }
            } : new List<SelectListItem>();
            
            result.AddRange(uow.MatchRepository.Get().Where(c=>c.ChampionshipId== ChampionshipId).Select(o => new SelectListItem()
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