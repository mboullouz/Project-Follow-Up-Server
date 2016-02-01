using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PUp.Models.Entity
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public partial class ProjectEntity:IBasicEntity
    {

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Finish { get; set; }
        public bool? Deleted { get; set; }
        
        public DateTime? EditAt { get; set; }

        /// <summary>
        /// A project may finish before estimated finish date time!
        /// </summary>
        public DateTime FinishAt { get; set; }
        public DateTime StartAt { get; set; }


        /// <summary>
        /// This the max date to finish the task, if not the task is considered undone 
        /// note: it is not related to the estimatedTimeInMinutes
        /// </summary>
        public DateTime EndAt { get; set; }


        /// <summary>
        /// define the benefits and related assumptions,associated 
        /// with the project.These may be quantitative or qualitative innature.
        /// Also, tracking and reporting of realized benefits is also described inthis section, 
        /// if applicable
        /// </summary>
        [Column(TypeName = "ntext")]
        public string Benifite { get; set; }

        /// <summary>
        /// Insert a summary of the project objectives and scopeformally agreed. 
        /// For example, what is the project trying to achieve? What scopedoes it cover?
        /// --TODO :handle this after :What functionalities or departments are involved?  Which are not involved
        /// </summary>
        [Column(TypeName = "ntext")]
        public string Objective { get; set; }

        public virtual ICollection<TaskEntity> Tasks { get; set; }
        public virtual ICollection<IssueEntity> Issues { get; set; }
        public virtual ICollection<ContributionEntity> Contributions { get; set; }

       

        public DateTime? DeleteAt { get; set; }


        public DateTime AddAt { get; set; }

        public ProjectEntity()
        {
            Tasks = new HashSet<TaskEntity>();
            Issues = new HashSet<IssueEntity>();
            Contributions = new HashSet<ContributionEntity>();
            Finish = false;
            AddAt = DateTime.Now;
            EditAt = DateTime.Now;
            StartAt = DateTime.Now.AddHours(1);
            EndAt = DateTime.Now.AddDays(7);
            FinishAt = DateTime.Now.AddDays(900); 
            Deleted = false;
            DeleteAt= DateTime.Now.AddYears(1);
        }


    }
}