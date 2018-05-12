using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PresentationLayer.Models.dbModels;
using PresentationLayer.Models.Repositories;

namespace PresentationLayer.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Champs()
        {
            return View();
        }

        public ActionResult Matches()
        {
            return View();
        }

        public ActionResult Gallery()
        {
            return View();
        }

        public ActionResult Goals()
        {
            return View();
        }

        #region Services
        //first 5 news//
        //most 5 popular//
        //latest 5 news//
        //pagination info
        //latest 5 in 3 sections



        //public ContentResult GetIndexData()
        //{
            
        //}
        #endregion
    }
}