using PUp.Models;
using PUp.Models.Entity;
using PUp.ViewModels.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PUp.ViewModels
{
    public class DashboardModelView
    {
        public List<TaskEntity> CurrentTasks = new List<TaskEntity>();
        public List<TaskEntity> OtherTasks = new List<TaskEntity>();
        public MatrixViewModel matrixVM {get;set;}
        public bool IsWorkingDayOver()
        {
            if (DateTime.Now.Hour < AppConst.DayStart || DateTime.Now.Hour > AppConst.DayEnd)
                return true;
            return false;
        }
        public AppConst AppConst { get; set; }
        public string TaskStatus(DateTime startDate,bool done,int dureationInHours)
        {
            string status = "";
            if (done)
            {
                status = "Done";
            }
            else if(!done && (startDate.Hour+dureationInHours)> AppConst.DayEnd)
            {
                status = "Task not finished in time!";
            }
            else
            {
                status = "In progress !";
            }
      
            return status;
        }
    }
}