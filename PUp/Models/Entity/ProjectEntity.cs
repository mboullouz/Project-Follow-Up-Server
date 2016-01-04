using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PUp.Models.Entity
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public partial  class ProjectEntity
    {

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Finish { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime EditAt { get; set; }
        /*
         * A project may finish before estimated finish date time!
         */
        public Nullable<DateTime> FinishAt { get; set; }
        public DateTime StartAt { get; set; }
        public Nullable<DateTime> EndAt { get; set; }

        public virtual ICollection<TaskEntity> Tasks { get; set; }
        public virtual UserEntity User { get; set; }

        public ProjectEntity()
        {
            Tasks = new HashSet<TaskEntity>();
            Finish = false;
            CreateAt = DateTime.Now;
            EditAt = DateTime.Now;
            StartAt = DateTime.Now.AddHours(1);
            EndAt = DateTime.Now.AddDays(7);
            FinishAt = EndAt;
        }

        
    }
}