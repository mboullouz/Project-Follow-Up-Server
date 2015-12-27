namespace PUp.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Entity;
    using Microsoft.AspNet.Identity.EntityFramework;
    public partial class DatabaseContext : IdentityDbContext<User>
    {
        public DatabaseContext()
            : base("name=PFModel")
        {
        }

        //public virtual DbSet<Entity.Project> Projects { get; set; }
        public virtual DbSet<Task> TaskSet { get; set; }
        public virtual DbSet<Project> ProjectSet { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });
        }
    }
}
