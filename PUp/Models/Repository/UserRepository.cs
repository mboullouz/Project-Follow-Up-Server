
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PUp.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PUp.Models.Repository
{
    public class UserRepository:AbstractRepository<UserEntity>
    {
        
        private UserManager<UserEntity> manager;

        public UserRepository():base() 
        {
            manager   = new UserManager<UserEntity>(new UserStore<UserEntity>(DbContext));
        }
        public UserRepository(DatabaseContext dbContext):base(dbContext)
        {
            manager = new UserManager<UserEntity>(new UserStore<UserEntity>(this.DbContext));
        }

        public DatabaseContext GetDbContext()
        {
            return this.DbContext;
        }
        

        public UserEntity GetCurrentUser()
        {
            // return   manager.FindByIdAsync(System.Web.HttpContext.Current.User.Identity.GetUserId());
            string currentUserId = System.Web.HttpContext.Current.User.Identity.GetUserId();
            UserEntity currentUser = DbContext.Users.FirstOrDefault(x => x.Id == currentUserId);
            return currentUser;
        }

        //TODO remove and use a Helper!
        public static List<NotificationEntity> CurrentUserNotifications()
        {
            // return   manager.FindByIdAsync(System.Web.HttpContext.Current.User.Identity.GetUserId());
            string currentUserId = System.Web.HttpContext.Current.User.Identity.GetUserId();
            DatabaseContext db= new DatabaseContext();
            UserEntity currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);
            if (currentUser == null)
                return new List<NotificationEntity>();
            return db.NotificationSet.Where(n => n.User.Id == currentUser.Id).ToList();
            
        }

        public string UsernameCurrent()
        {
            return System.Web.HttpContext.Current.User.Identity.GetUserName();
        }
 
        public void SetDbContext(DatabaseContext dbContext)
        {
            this.DbContext = dbContext;
        }

        public UserEntity FindByStringId(int id)
        {
            throw new Exception("User Identity accepts only String as Id! ");
            
        }
        public  UserEntity FindById(string id)
        {
            return DbContext.Users.FirstOrDefault(v => v.Id == id);
        }

        public void AddContribution(ContributionEntity c)
        {
            GetCurrentUser().Contributions.Add(c);
            DbContext.SaveChanges();
        }

        public UserEntity GetFirstOrDefault()
        {
            return DbContext.Users.FirstOrDefault();
        }

        public UserEntity FindByEmail(string email)
        {
            return DbContext.Users.First(u => u.Email == email);
        }

       

        public override void MarkDeleted(UserEntity e)
        {
            throw new NotImplementedException();
        }

        public override UserEntity FindById(int id)
        {
            throw new NotImplementedException();
        }
    }
}