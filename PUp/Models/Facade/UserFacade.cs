
using Microsoft.AspNet.Identity;
using PUp.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PUp.Models.Facade
{
    public class UserFacade:IGenericFacade<User>
    {
        private DatabaseContext dbContext;
        public UserFacade() 
        {
            dbContext = new DatabaseContext();
        }

        public void Add(User u)
        {
             /**
               todo: user UserIdentity to create user properly !
             */
             
            dbContext.SaveChanges();
        }

        public string UsernameCurrent()
        {
            return System.Web.HttpContext.Current.User.Identity.GetUserName();
        }
         

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// @TODO : Get Users properly!
        /// </summary>
        /// <returns></returns>
        public List<User> GetAll()
        {   
            
            return new List<User>();
        }

        public void remove(User e)
        {
            throw new NotImplementedException();
        }
    }
}