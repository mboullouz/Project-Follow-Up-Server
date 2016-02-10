using PUp.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PUp.ViewModels
{
    public class DashboardModelView
    {
        public List<TaskEntity> CurrentTasks = new List<TaskEntity>();
        public List<TaskEntity> OtherTasks   = new List<TaskEntity>();
    }
}