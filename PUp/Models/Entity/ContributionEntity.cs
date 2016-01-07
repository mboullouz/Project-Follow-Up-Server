using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PUp.Models.Entity
{
    public class ContributionEntity
    {
        [Key]
        public int Id { get; set; }
        //TODO use an enum
        public string Role { get; set; }
 
        public DateTime StartAt { get; set; }

        //Default equal to end of the project!
        public DateTime EndAt { get; set; }
        public UserEntity User { get; set; }
        public ProjectEntity Project { get; set; }

        public ContributionEntity()
        {
            StartAt = DateTime.Now;
        }
    }
}