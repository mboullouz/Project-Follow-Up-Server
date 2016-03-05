namespace PUp.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Entity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Data.Entity.Core.Objects;
    using System.Data.Entity.Validation;
    public partial class DatabaseContext : IdentityDbContext<UserEntity>
    {
        public DatabaseContext() : base("name=PFModel")
        {
        }


        public   DbSet<TaskEntity> TaskSet { get; set; }
        public   DbSet<ProjectEntity> ProjectSet { get; set; }
        public   DbSet<NotificationEntity> NotificationSet { get; set; }
       
        public virtual DbSet<IssueEntity> IssueSet { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });
        }

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                var newException = new FormattedDbEntityValidationException(e);
                throw newException;
            }
        }


    }
}
