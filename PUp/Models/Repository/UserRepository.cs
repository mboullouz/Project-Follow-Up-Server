
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PUp.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PUp.Models.Repository
{
    public class UserRepository:IUserRepository
    {
        private DatabaseContext dbContext;
        private UserManager<UserEntity> manager;

        public UserRepository() 
        {
            dbContext = new DatabaseContext();
            manager   = new UserManager<UserEntity>(new UserStore<UserEntity>(dbContext));
        }
        public DatabaseContext GetDbContext()
        {
            return this.dbContext;
        }
        public void Add(UserEntity u)
        {
             /**
              * todo: user UserIdentity to create user properly !
              */
             
            dbContext.SaveChanges();
        }

        public UserEntity GetCurrentUser()
        {
            // return   manager.FindByIdAsync(System.Web.HttpContext.Current.User.Identity.GetUserId());
            string currentUserId = System.Web.HttpContext.Current.User.Identity.GetUserId();
            UserEntity currentUser = dbContext.Users.FirstOrDefault(x => x.Id == currentUserId);
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
        /// 
        /// </summary>
        /// <returns></returns>
        public List<UserEntity> GetAll()
        {   
            
            return dbContext.Users.ToList();
        }

        public void Remove(UserEntity e)
        {
            throw new NotImplementedException();
        }

        public void SetDbContext(DatabaseContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public UserEntity FindById(int id)
        {
            throw new Exception("User Identity accepts only String as Id! ");
            
        }
        public UserEntity FindById(string id)
        {
            return dbContext.Users.FirstOrDefault(v => v.Id == id);
        }
    }
}