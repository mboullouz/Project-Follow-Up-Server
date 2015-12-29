using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PUp.Models.Facade
{
    public interface IGenericFacade<E>:IDisposable
    {
        List<E> GetAll();
        void Add(E e);
        void Remove(E e);
        void SetDbContext(DatabaseContext dbContext);
        DatabaseContext GetDbContext();
    }
}