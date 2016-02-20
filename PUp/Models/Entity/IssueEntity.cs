using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PUp.Models.Entity
{
    /// <summary>
    /// Track issues related to the execution of a project!
    /// </summary>
    public class IssueEntity : IBasicEntity
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "ntext")]
        public string Description { get; set; }

        public string Status { get; set; }
        public bool? Deleted { get; set; }

        public DateTime? EditAt { get; set; }

        /// <summary>
        /// relatedArea: Issues can arise in one or more of the followingareas: 
        /// budget, schedule, resources, or quality of work.]
        /// </summary>
        public string RelatedArea { get; set; }

        public ProjectEntity Project { set; get; }

        public DateTime AddAt { get; set; }

        public DateTime? DeleteAt { get; set; }

        public UserEntity Submitter { get; set; }
    }
}