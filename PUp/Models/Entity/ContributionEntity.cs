using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PUp.Models.Entity
{
    public class ContributionEntity:IBasicEntity
    {
        
        [Required]
        public int ProjectId { get; set; }
        [Required]
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
            Role = RolesToString((int)Roles.TaskAdd);
        }
        public enum Roles:int { FisrstContributor = 0, AssignedTo, TaskAdd, IssueSubmit };

        public static string RolesToString(int roleId)
        {           
            switch (roleId)
            {
                case (int)Roles.AssignedTo :return "AssignedTo";
                case (int)Roles.FisrstContributor: return "FisrstContributor";
                case (int)Roles.IssueSubmit: return "IssueSubmit";
                case (int)Roles.TaskAdd: return "TaskAdd";
                default:
                    throw new System.ComponentModel.InvalidEnumArgumentException();            
            }
        }
    }

}