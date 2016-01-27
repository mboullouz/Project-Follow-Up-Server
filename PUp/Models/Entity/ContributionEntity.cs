using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PUp.Models.Entity
{
    public class ContributionEntity:IBasicEntity
    {
        

        public int ProjectId { get; set; }
        public string UserId { get; set; }
        //TODO use an enum
        public string Role { get; set; }
 
        public DateTime AddAt { get; set; }
        public bool? Deleted { get; set; }

        //Default equal to end of the project!
        public Nullable<DateTime> EndAt { get; set; }
        public UserEntity User { get; set; }
        public ProjectEntity Project { get; set; }

        public DateTime? EditAt
        {
            get; set;
        }

        public DateTime? DeleteAt
        {
            get; set;
        }

        public ContributionEntity()
        {
            AddAt = DateTime.Now;
            Deleted = false;
            
        }
    }
}