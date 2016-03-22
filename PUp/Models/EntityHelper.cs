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
        /**
        Useful when serializing/deserializing object
        */
        public const int MaxDepth = 3;

        public const int DayStart = 0;
        public const int DayEnd = 23;
        public const int HoursPerDay = 10;
        public const int DaysPerWeek = 5;

        public static List<int> ToArray()
        {
            var list = new List<int>();
            list.Add(DayStart);
            list.Add(DayEnd);
            list.Add(HoursPerDay);
            list.Add(DaysPerWeek);
            return list;
        }
    }

    public class IssueStatus
    {
        public const string Open = "Open";
        public const string Resolved = "Resolved";
    }


}