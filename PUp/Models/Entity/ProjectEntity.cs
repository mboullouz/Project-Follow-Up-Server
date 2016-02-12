﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PUp.Models.Entity
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public partial class ProjectEntity : IBasicEntity
    {

        [Key]
        public int Id { get; set; }

        [MinLength(10)]
        public string Name { get; set; }
        public bool Finish { get; set; }
        public bool? Deleted { get; set; }

        public DateTime? EditAt { get; set; }

        /// <summary>
        /// A project may finish before estimated finish date time!
        /// default finishedAt= endAt
        /// </summary>
        public DateTime FinishedAt { get; set; }
        public DateTime StartAt { get; set; }


        /// <summary>
        /// Project owner: default=>project creator!
        /// </summary>
        [Required]
        public UserEntity Owner { get; set; }


        /// <summary>
        /// Contributors in the project includs Owner
        /// </summary>
        public ICollection<UserEntity> Contributors { get; set; }

        /// <summary>
        /// This the max date to finish the project, if not the task is considered undone 
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
        [MinLength(100)]
        [MaxLength(100000)]
        public string Benifite { get; set; }

        /// <summary>
        /// Insert a summary of the project objectives and scopeformally agreed. 
        /// For example, what is the project trying to achieve? What scopedoes it cover?
        /// --TODO :handle this after :What functionalities or departments are involved?  Which are not involved
        /// </summary>
        [Column(TypeName = "ntext")]
        [MinLength(100)]
        [MaxLength(100000)]
        public string Objective { get; set; }

        public virtual ICollection<TaskEntity> Tasks { get; set; }
        public virtual ICollection<IssueEntity> Issues { get; set; }
       

        public DateTime? DeleteAt { get; set; }


        public DateTime AddAt { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProjectEntity()
        {
            Tasks = new HashSet<TaskEntity>();
            Issues = new HashSet<IssueEntity>();
            Contributors = new HashSet<UserEntity>();
            Finish = false;
            AddAt = DateTime.Now;
            EditAt = DateTime.Now;
            StartAt = DateTime.Now.AddHours(1);
            EndAt = DateTime.Now.AddDays(7);
            FinishedAt = DateTime.Now.AddDays(900);
            Deleted = false;
            DeleteAt = DateTime.Now.AddYears(2);
        }


    }
}