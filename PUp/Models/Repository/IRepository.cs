﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PUp.Models.Repository
{
    public interface IRepository<E>:IDisposable
    {
        List<E> GetAll();
        void Add(E e);
        void Remove(E e);
        E FindById(int id);
        void SetDbContext(DatabaseContext dbContext);
        DatabaseContext GetDbContext();
    }
}