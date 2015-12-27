using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PUp.Models.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PUp.Models
{
    public class DatabaseInitializer : DropCreateDatabaseAlways<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
            var UserManager = new UserManager<Entity.User>(new

                                           UserStore<User>(context));

            var RoleManager = new RoleManager<IdentityRole>(new
                                     RoleStore<IdentityRole>(context));

            string name = "med";
            string password = "medmed";
            string roleName = "ADMIN";
            string userEmail = "mohamed.boullouz@gmail.com";

            //Create Role Admin if it does not exist
            if (!RoleManager.RoleExists(roleName))
            {
                var roleresult = RoleManager.Create(new IdentityRole(roleName));
            }

            //Create User=med with password=medmed
            var user = new User();
            user.UserName = name;
            user.Email = userEmail;
            var adminresult = UserManager.Create(user, password);

            //Add User med to Role ADMIN
            if (adminresult.Succeeded)
            {
                var result = UserManager.AddToRole(user.Id, roleName);
            }
            base.Seed(context);
        }
    }
}