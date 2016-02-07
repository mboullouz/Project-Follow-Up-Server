using PUp.Models.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PUp.ViewModels
{
    public class AddProjectViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} length must be betweeb {2} and  {1}  caracters.", MinimumLength = 10)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime StartAt { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime EndAt { get; set; }
        public int Id { get; set; }

        [Required]
        [StringLength(100000, ErrorMessage = "The {0} must be minimum {2} caracters.", MinimumLength = 100)]
        public string Benifite { get; set; }

        [Required]
        [StringLength(100000, ErrorMessage = "The {0} must be minimum {2} caracters.", MinimumLength = 100)]
        public string Objective { get; set; }

        public AddProjectViewModel()
        {

        }

        //For edit case
        public AddProjectViewModel(ProjectEntity p)
        {
            Name = p.Name;
            StartAt = p.StartAt;
            EndAt = p.EndAt;
            Benifite = p.Benifite;
            Objective = p.Objective;
        }
    }
}