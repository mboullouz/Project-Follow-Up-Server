
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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
        private UserManager<User> manager;

        public UserFacade() 
        {
            dbContext = new DatabaseContext();
            manager   = new UserManager<User>(new UserStore<User>(dbContext));
        }
        public DatabaseContext GetDbContext()
        {
            return this.dbContext;
        }
        public void Add(User u)
        {
             /**
               todo: user UserIdentity to create user properly !
             */
             
            dbContext.SaveChanges();
        }

        public User GetCurrentUser()
        {
            // return   manager.FindByIdAsync(System.Web.HttpContext.Current.User.Identity.GetUserId());
            string currentUserId = System.Web.HttpContext.Current.User.Identity.GetUserId();
            User currentUser = dbContext.Users.FirstOrDefault(x => x.Id == currentUserId);
            return currentUser;
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

        public void SetDbContext(DatabaseContext dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}