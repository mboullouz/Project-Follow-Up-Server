using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PUp.Models
{
    public class LevelFlag
    {   
        public const int Info    = 0;
        public const int Warning = 1;
        public const int Danger  = 2;       
    }

    public class AppConst
    {
        public const int HoursPerDay = 8;
        public const int DaysPerWeek = 5;
    }

    public class IssueStatus
    {
        public const string Open = "Open";
        public const string Resolved = "Resolved";
    }

    
}