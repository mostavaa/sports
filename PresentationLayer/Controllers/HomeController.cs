using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PresentationLayer.Models;
using PresentationLayer.Models.Repositories;
using PresentationLayer.Models.ViewModels;

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

        public ContentResult GetAllChampionships()
        {
            return ToJson(AllChampionships(addNullEntry:false).Select(o => new Lookup()
            {
                Key = o.Value,
                Value = o.Text,
            }));
        }
        public ContentResult InitHome(long championshipId = 0)
        {

            UnitOfWork uow = new UnitOfWork();

            List<NewsVM> latest5News = new List<NewsVM>();
            foreach (var news in uow.NewsRepository.GetLatest5News(championshipId))
            {
                latest5News.Add(new NewsVM()
                {
                    date = news.CreatedTime.ToString(Common.DateFormat),
                    desc = news.NewsHeading,
                    imageName = news.GUID + ".png",
                    link = "single.html"
                });
            }
            var latestNews = new List<object>();
            if(championshipId<1)
                foreach (var champ in uow.NewsRepository.GetLatestNews())
            {
                List<NewsVM> champNews = new List<NewsVM>();
                foreach (var news in champ.Value)
                {
                    champNews.Add(new NewsVM()
                    {
                        date = news.CreatedTime.ToString(Common.DateFormat),
                        desc = news.NewsHeading,
                        imageName = news.GUID + ".png",
                        link = "single.html"
                    });
                }
                    latestNews.Add(new
                    {
                        champ.Key.ChampName,
                        News= champNews
                    });
            }

            List<NewsVM> mostPopular = new List<NewsVM>();
            foreach (var news in uow.NewsRepository.GetMostPopularIndex(championshipId))
            {
                mostPopular.Add(new NewsVM()
                {
                    date = news.CreatedTime.ToString(Common.DateFormat),
                    desc = news.NewsHeading,
                    imageName = news.GUID + ".png",
                    link = "single.html"
                });
            }

            List<NewsVM> allNews = new List<NewsVM>();
            foreach (var news in uow.NewsRepository.GetNews(0, championshipId))
            {
                allNews.Add(new NewsVM()
                {
                    date = news.CreatedTime.ToString(Common.DateFormat),
                    desc = news.NewsHeading,
                    imageName = news.GUID + ".png",
                    link = "single.html"
                });
            }
            
            var result = new
            {
                latest5News,
                latestNews,
                mostPopular,
                allNews,
            };


            return ToJson(new
            {
                result
            });
        }

        public ContentResult GetNews(int id=0, long championshipId = 0)
        {
            if (id > 0)
                id--;
            UnitOfWork uow = new UnitOfWork();
            List<NewsVM> allNews = new List<NewsVM>();
            foreach (var news in uow.NewsRepository.GetNews(id, championshipId))
            {
                allNews.Add(new NewsVM()
                {
                    date = news.CreatedTime.ToString(Common.DateFormat),
                    desc = news.NewsHeading,
                    imageName = news.GUID + ".png",
                    link = "single.html"
                });
            }
            var result = new
            {
                allNews,
            };


            return ToJson(new
            {
                result
            });
        }
    }
}