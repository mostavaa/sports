using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using PresentationLayer.Models.dbModels;

namespace PresentationLayer.Models.Repositories
{
    public class NewsRepository:Repository<News>
    {
        public NewsRepository(ApplicationDbContext context) : base(context)
        {

        }
        /// <summary>
        /// return champ , and list of selected news
        /// </summary>
        /// <returns></returns>
        public Dictionary<Championship, List<News>> GetLatestNews()
        {
            Dictionary<long, List<News>> Latest = new Dictionary<long, List<News>>();
            long MaxId = _context.Championships.Max(o => o.Id);
            long MinId = _context.Championships.Min(o => o.Id);
            Random Rand = new Random();
            int SelectedId = 0;
            UnitOfWork uow = new UnitOfWork();
            bool champFound = false;
            for (int i = 0; i <Common.NumOfHomeSections; i++)
            {
                do
                {
                    champFound = false;
                    SelectedId = Rand.Next((int)MinId, (int)MaxId);
                    Championship champ = uow.ChampionshipRepository.Get(o => o.Id == SelectedId).FirstOrDefault();
                    if (champ != null)
                    {
                        if (!Latest.ContainsKey(champ.Id))
                        {
                            champFound = true;
                            Latest.Add(champ.Id , champ.News.OrderByDescending(o=>o.CreatedTime).Take(Common.NumOfNewsInHomeSection).ToList());
                        }
                    }
                } while (!champFound);
                if (Common.NumOfHomeSections >uow.ChampionshipRepository.Get().Count())
                    break;
            }
            var Result = new Dictionary<Championship, List<News>>();
            foreach (var item in Latest)
            {
                    Result.Add(uow.ChampionshipRepository.GetById(item.Key) , item.Value);
            }
            return Result;
        }
        public List<News> GetLatest5News(long championshipId=0)
        {
            return championshipId > 0
                ? Get(o => o.ChampionshipId == championshipId).Take(5).ToList()
                : Get().Take(5).ToList();
            
        }

        public List<News> GetMostPopularIndex(long championshipId=0)
        {
            DateTime nexMonth = DateTime.Now.AddMonths(Common.NumOfOldMonths);
            return championshipId > 0
                ? Get(o => o.CreatedTime > nexMonth && o.ChampionshipId == championshipId)
                    .OrderByDescending(x => x.Stars)
                    .Take(Common.NumOfPopular).ToList()
                : Get(o => o.CreatedTime > nexMonth)
                    .OrderByDescending(x => x.Stars)
                    .Take(Common.NumOfPopular).ToList();
           
        }

        public List<News> GetLatestNewsHomePage()
        {
            return Get().Take(Common.HomePageNumOfNews).ToList();
        }

        public List<News> GetNews(int offset=0 , long championshipId = 0)
        {
            return championshipId > 0
                ? Get(o => o.ChampionshipId == championshipId).Skip(offset).Take(Common.DefaultTake).ToList()
                : Get().Skip(offset).Take(Common.DefaultTake).ToList();


        }
    }
}