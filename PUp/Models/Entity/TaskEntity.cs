namespace PUp.Models.Entity
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public partial class TaskEntity:IBasicEntity
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }

        [Column(TypeName = "ntext")]
        public string Description { get; set; }

        public bool Done { get; set; }
        public DateTime AddAt { get; set; }
        public DateTime? EditAt { get; set; }
        public Nullable<DateTime> FinishAt { get; set; }

        /// <summary>
        /// High priority mean must be executed as soon as possible 
        /// example Priority = 0 => low priority
        ///         Priority = 1 => normal 
        ///         Priority = 2 => High
        /// </summary>
        public Nullable<int> Priority { get; set; }

        /// <summary>
        /// What are the keyTasks in the project that have more impact?
        /// --not related to the priority 
        /// </summary>
        public bool keyFactor { get; set; }

        /// <summary>
        /// Estimated time in minutes 
        /// its NOT equivalent to the difference between start date and finish date
        /// </summary>
        public int estimatedTimeInMinutes { get; set; }
        public DateTime? DeleteAt { get; set; }
        public bool? Deleted { get; set; }
        public TaskEntity()
        {
            Done = false;
            Priority = 1;
            AddAt = DateTime.Now;
            EditAt = DateTime.Now;
            keyFactor = false;
            estimatedTimeInMinutes = 60;
            Deleted = false;
        }

        public virtual ProjectEntity Project { get; set; }
 

       
    }
}