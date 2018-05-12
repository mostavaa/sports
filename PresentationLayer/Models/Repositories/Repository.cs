using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq.Expressions;

namespace PresentationLayer.Models.Repositories
{
    public class Repository<T> where T : class
    {
        private readonly ApplicationDbContext context;
        private DbSet<T> Entities;
        string errorMessage = string.Empty;

        public Repository()
        {
            
        }
        public Repository(ApplicationDbContext context)
        {
            this.context = context;
            this.Entities = context.Set<T>();
        }

        public virtual IEnumerable<T> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<T> query = Entities;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public virtual T GetByID(params object [] id)
        {
            return Entities.Find(id);
        }

        public virtual void Insert(T entity)
        {
            Entities.Add(entity);
        }

        public virtual void Delete(long id)
        {
            T entityToDelete = Entities.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(T entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                Entities.Attach(entityToDelete);
            }
            Entities.Remove(entityToDelete);
        }

        public virtual void Update(T entityToUpdate)
        {
            Entities.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }



    }
}



    
