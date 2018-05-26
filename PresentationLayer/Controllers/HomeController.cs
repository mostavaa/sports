using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PresentationLayer.Models;
using PresentationLayer.Models.dbModels;
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

        public ContentResult GetGoals(int page =0 , long matchId = 0)
        {
            if (page > 0)
                page--;
            UnitOfWork uow = new UnitOfWork();
            Dictionary<Match , List<NewsVM>> GoalMatches = new Dictionary<Match, List<NewsVM>>();
            foreach (var goal in uow.GoalRepository.GetGoals(page, matchId))
            {
                var newsvm = new NewsVM()
                {
                    date = goal.CreatedTime.ToString(Common.DateFormat),
                    desc = goal.GoalPlayerName,
                    imageName = goal.GUID + ".png",
                    link = "single.html"
                };
                if (GoalMatches.ContainsKey(goal.Match))
                {
                    if (GoalMatches[goal.Match].Count > 0)
                        GoalMatches[goal.Match].Add(newsvm);
                    else GoalMatches[goal.Match] = new List<NewsVM>() {newsvm};
                }
                else
                {
                    GoalMatches.Add(goal.Match , new List<NewsVM>()
                    {
                        newsvm
                    });
                }
            }
            var output= new List<object>();
            foreach (var item in GoalMatches)
            {
                List<NewsVM> goals = new List<NewsVM>();
                foreach (var goal in item.Value)
                {
                    goals.Add(goal);
                }
                output.Add(new
                {
                    heading = item.Key.FirstTeamName + "-" + item.Key.SecondTeamName,
                    goals
                });
            }
            var result = new
            {
              allGoals = output,
            };


            return ToJson(new
            {
                result
            });
        }

        public ContentResult GetAlbums(int id = 0)
        {
            if (id > 0)
                id--;
            UnitOfWork uow = new UnitOfWork();
            List<NewsVM> allAlbums = new List<NewsVM>();
            foreach (var album in uow.AlbumRepository.GetAlbums(id))
            {
                allAlbums.Add(new NewsVM()
                {
                    date = album.CreatedTime.ToString(Common.DateFormat),
                    desc = album.AlbumName,
                    imageName = album.GUID + ".png",
                    link = "single.html"
                });
            }
            var result = new
            {
                allAlbums,
            };


            return ToJson(new
            {
                result
            });
        }

        public ContentResult GetMatches(DateTime dateTime)
        {
            UnitOfWork uow = new UnitOfWork();
            Dictionary<string, List<MatchVM>> ChampionshipMatches = new Dictionary<string, List<MatchVM>>();
            foreach (var match in uow.MatchRepository.GetMatches(dateTime))
            {
                var matchVm = new MatchVM()
                {
                    link= "single.html",
                    firstTeam= match.FirstTeamName,
                    secondTeam= match.SecondTeamName,
                    firstTeamGoals= match.FirstTeamName.ToString() ,
                    secondTeamGoals= match.SecondTeamGoals.ToString(),
                    date= match.MatchDateTime.ToString(Common.DateFormat),
                    isPlayed= match.IsPlayed
                };
                if (match.ChampionshipId == null)
                {
                    if (ChampionshipMatches.ContainsKey(""))
                    {
                        if (ChampionshipMatches[""].Count > 0)
                            ChampionshipMatches[""].Add(matchVm);
                        else ChampionshipMatches[""] = new List<MatchVM>() { matchVm };
                    }
                    else
                    {
                        ChampionshipMatches.Add("", new List<MatchVM>()
                        {
                            matchVm
                        });
                    }
                }
                else
                {
                    if (ChampionshipMatches.ContainsKey(match.Championship.ChampName))
                    {
                        if (ChampionshipMatches[match.Championship.ChampName].Count > 0)
                            ChampionshipMatches[match.Championship.ChampName].Add(matchVm);
                        else ChampionshipMatches[match.Championship.ChampName] = new List<MatchVM>() { matchVm };
                    }
                    else
                    {
                        ChampionshipMatches.Add(match.Championship.ChampName, new List<MatchVM>()
                        {
                            matchVm
                        });
                    }
                }
            }
            var output = new List<object>();
            foreach (var item in ChampionshipMatches)
            {
                List<MatchVM> matches = new List<MatchVM>();
                foreach (var match in item.Value)
                {
                    matches.Add(match);
                }
                output.Add(new
                {
                    Name = item.Key,
                    matches = matches
                });
            }
            var result = new
            {
                AllMatches = output,
            };


            return ToJson(new
            {
                result
            });
        }
    }
}