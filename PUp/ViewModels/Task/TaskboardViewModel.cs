using Newtonsoft.Json;
using PUp.Models.Entity;
using PUp.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PUp.ViewModels.Task
{
    public class TaskboardViewModel : BaseModelView
    {
        public List<TaskDto> InProgress { get; set; }
        public List<TaskDto> Complete { get; set; }
        public List<TaskDto> Upcoming { get; set; }

        public ProjectDto Project { get; set; }

 
        public TaskboardViewModel()
        {
        }

         
    }
}