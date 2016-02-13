using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PUp.Models.Repository
{
    public abstract class AbstractRepository<E> where E:class 
    {
        public DatabaseContext DbContext { get; set; }

        public AbstractRepository(DatabaseContext dbContext)
        {
            DbContext = dbContext;
        }
        public AbstractRepository( )
        {
            DbContext = new DatabaseContext();
        }

        public void Add(E e) {
            DbContext.Set<E>().Add(e);
            DbContext.SaveChanges();
        } 
        
        public List<E> GetAll()
        {
            return DbContext.Set<E>().ToList();
        }
        public abstract E FindById(int id);
        public abstract void MarkDeleted(E e);
        public virtual void Remove(E e)
        {
            DbContext.Set<E>().Remove(e);
            DbContext.SaveChanges();
        }


        public void Dispose()
        {
            DbContext.Dispose();
        }


    }
}