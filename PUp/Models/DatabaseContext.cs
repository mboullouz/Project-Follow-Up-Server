namespace PUp.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Entity;
    using Microsoft.AspNet.Identity.EntityFramework;
    public partial class DatabaseContext : IdentityDbContext<UserEntity>
    {
        public DatabaseContext()
            : base("name=PFModel")
        {
        }

        
        public virtual DbSet<TaskEntity> TaskSet { get; set; }
        public virtual DbSet<ProjectEntity> ProjectSet { get; set; }
        public virtual DbSet<NotificationEntity> NotificationSet { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });
        }
    }
}
