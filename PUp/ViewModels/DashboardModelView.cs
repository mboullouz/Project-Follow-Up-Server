using Newtonsoft.Json;
using PUp.Models;
using PUp.Models.Dto;
using PUp.ViewModels.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PUp.ViewModels
{
    public class DashboardModelView : BaseModelView
    {
        public List<TaskDto> CurrentTasks = new List<TaskDto>();
        public List<TaskDto> OtherTasks = new List<TaskDto>();
        public List<TaskDto> TodayDoneTasks = new List<TaskDto>();


        public MatrixViewModel MatrixVM { get; set; }
        public bool IsWorkingDayOver
        {
            get
            {
                if (DateTime.Now.Hour < AppConst.DayStart || DateTime.Now.Hour > AppConst.DayEnd)
                    return true;
                return false;
            }

        }
        public AppConst Consts
        {
            get
            {
                return new AppConst();
            }
        }

        /**
        Move this to the client side!
        */
        public string ComputeTaskStatus(DateTime startDate, bool done, int dureationInHours)
        {
            string status = "";
            if (done)
            {
                status = "Done";
            }
            else if (!done && (startDate.Hour + dureationInHours) > AppConst.DayEnd)
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