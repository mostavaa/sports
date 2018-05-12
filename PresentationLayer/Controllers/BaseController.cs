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