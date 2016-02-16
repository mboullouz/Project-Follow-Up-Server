﻿using PUp.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PUp.ViewModels.Task
{
    public class TaskViewModel
    {
        public List<TaskEntity> ActiveTasks { get; set; }
        public List<TaskEntity> DeletedTasks { get; set; }
        public ProjectEntity Project { get; set; }
        public TaskViewModel(ProjectEntity p)
        {
            Project = p;
            ActiveTasks = p.Tasks.Where(t => t.Deleted  == false).ToList();
            DeletedTasks = p.Tasks.Where(t => t.Deleted == true).ToList();
        }

        public TaskViewModel()
        {

        }

    }

     
}