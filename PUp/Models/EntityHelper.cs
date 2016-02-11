using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PUp.Models
{
    public class LevelFlag
    {   
        public const int INFO    = 0;
        public const int WARNING = 1;
        public const int DANGER  = 2;       
    }

    public class AppConst
    {
        public const int HOURS_PER_DAY = 8;
        public const int DAYS_PER_WEEK = 5;
    }

    public class RoleContribution
    {
        public  const int  FIRST_CONTRIBUTOR = 0, TASK_ASSIGNED_TO=1, TASK_ADD=2, ISSUE_SUBMIT=3 ;

        public static string FlagToString(int roleId)
        {
            switch (roleId)
            {
                case FIRST_CONTRIBUTOR: return "First Contributor";
                case TASK_ASSIGNED_TO: return "Task Assigned To";
                case TASK_ADD: return "Task Add";
                case ISSUE_SUBMIT: return "Issue Submit";
               
                default:
                    throw new System.ComponentModel.InvalidEnumArgumentException();
            }
        }
    }
}