using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using PresentationLayer.Models.dbModels;

namespace PresentationLayer.Models.Repositories
{
    public class NewsRepository:Repository<News>
    {
        public ApplicationDbContext Context { get; }

        public NewsRepository()
        {
           
        }
        public NewsRepository(ApplicationDbContext ctx):this()
        {
            Context = ctx;
        }
        /// <summary>
        /// return champ , and list of selected news
        /// </summary>
        /// <returns></returns>
        public Dictionary<Championship, List<News>> GetLatestNews()
        {
            Dictionary<Guid, List<News>> Latest = new Dictionary<Guid, List<News>>();
            long MaxId = Context.Championships.Max(o => o.Id);
            long MinId = Context.Championships.Min(o => o.Id);
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
                        if (!Latest.ContainsKey(champ.GUID))
                        {
                            champFound = true;
                            Latest.Add(champ.GUID , champ.News.OrderByDescending(o=>o.CreatedTime).Take(Common.NumOfNewsInHomeSection).ToList());
                        }
                    }
                } while (!champFound);
            }
            var Result = new Dictionary<Championship, List<News>>();
            foreach (var item in Latest)
            {
                    Result.Add(uow.ChampionshipRepository.GetByID(item.Key) , item.Value);
            }
            return Result;
        }
        public List<News> GetLatest5News()
        {
            return Get().Take(5).ToList();
        }

        public List<News> GetMostPopularIndex()
        {
            return Get(o => o.CreatedTime > DateTime.Now.AddMonths(Common.NumOfOldMonths), o => o.OrderByDescending(x => x.Stars))
                .Take(Common.NumOfPopular).ToList();
        }

        public List<News> GetLatestNewsHomePage()
        {
            return Get().Take(Common.HomePageNumOfNews).ToList();
        }

        public List<News> GetNews(int offset=0)
        {
            return Get().Skip(offset).Take(Common.DefaultTake).ToList();
        }
    }
}