using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PUp.Models.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public partial class TaskEntity { 
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Nullable<int> Priority { get; set; }
        public Nullable<int> EditionNumber { get; set; }
        public bool Done { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime EditAt { get; set; }
        public Nullable<DateTime> FinishAt { get; set; }
        public bool Deleted { get; set; }
        public TaskEntity()
        {
            Done = false;
            CreateAt = DateTime.Now;
            EditAt = DateTime.Now;
            Deleted = false;
        }

        public virtual ProjectEntity Project { get; set; }
    }
}