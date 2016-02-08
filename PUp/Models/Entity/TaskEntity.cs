namespace PUp.Models.Entity
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public partial class TaskEntity:IBasicEntity
    {
        [Key]
        public int Id { get; set; }
        [MinLength(10)]
        public string Title { get; set; }

        [Column(TypeName = "ntext")]
        [MinLength(30)]
        public string Description { get; set; }

        public bool Done { get; set; }


        /// <summary>
        /// Use Urgent/Important matrix
        /// </summary>
        public bool Urgent { get; set; }
        public bool Critical { get; set; }

        /// <summary>
        /// A Task may be postponed depending on the U/I Matrix
        /// </summary>
        public bool Postponed { get; set; }

        public DateTime AddAt { get; set; }

        

        public DateTime? EditAt { get; set; }
        public Nullable<DateTime> FinishAt { get; set; }

        /// <summary>
        /// What are the keyTasks in the project that have more impact?
        /// --not related to the priority 
        /// </summary>
        public bool KeyFactor { get; set; }

        public DateTime StartAt { get; set; }
        public DateTime EndAt  { get; set; }

     

        /// <summary>
        /// The time spent on the task, default equal estimated
        /// </summary>
        ///public int SpentTimeInMinutes { get; set; }

        public DateTime? DeleteAt { get; set; }
        public bool? Deleted { get; set; }

        [Required]
        public UserEntity AssignedBy { set; get; }
        public UserEntity Executor { set; get; }

        public TaskEntity()
        {
            Done = false;
            Postponed = false;
            Urgent = false;
            Critical = false;
            AddAt = DateTime.Now;
            EditAt = DateTime.Now;
            KeyFactor = false;
            
            Deleted = false;
        }

        public virtual ProjectEntity Project { get; set; }
    }
}