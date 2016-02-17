using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PUp.Models
{
    public class LevelFlag
    {
        public const int Info = 0;
        public const int Warning = 1;
        public const int Danger = 2;
        public const int Success = 3;
    }

    public class AppConst
    {
        public const int HoursPerDay = 10;
        public const int DayStart = 8;
        public const int DayEnd = 17;
        public const int DaysPerWeek = 5;
    }

    public class IssueStatus
    {
        public const string Open = "Open";
        public const string Resolved = "Resolved";
    }


}