using System;

namespace PresentationLayer.Models.Repositories
{
    public class UnitOfWork : IDisposable
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        public ChampionshipRepository ChampionshipRepository ;
        public NewsRepository NewsRepository ;
        public MatchRepository MatchRepository;
        public AlbumRepository AlbumRepository;
        public GoalRepository GoalRepository;
        public AttachmentRepository AttachmentRepository;


        public UnitOfWork()
        {
            ChampionshipRepository = new ChampionshipRepository(_context);
            NewsRepository = new NewsRepository(_context);
            MatchRepository = new MatchRepository(_context);
            AlbumRepository = new AlbumRepository(_context);
            GoalRepository = new GoalRepository(_context);
            AttachmentRepository = new AttachmentRepository(_context);
        }

        public void Save()
        {
            _context.SaveChanges(); 
        }

        private bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}