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

        /**
         Use in Json Object because consts are not serialized properly!
        */
        public List<SimpleKeyValue<string, int>> ListConsts
        {
            get
            {
                var list = new List<SimpleKeyValue<string, int>>();
                list.Add(new SimpleKeyValue<string, int> { Key = "DayStart", Value = DayStart });
                list.Add(new SimpleKeyValue<string, int> { Key = "DayEnd", Value = DayEnd });
                list.Add(new SimpleKeyValue<string, int> { Key = "HoursPerDay", Value = HoursPerDay });
                list.Add(new SimpleKeyValue<string, int> { Key = "DaysPerWeek", Value = DaysPerWeek });
                return list;
            }
        }
 
    }

    public class IssueStatus
    {
        public const string Open = "Open";
        public const string Resolved = "Resolved";
    }


    /// <summary>
    /// PUp media types as consts
    /// </summary>
    public class AppMediaType
    {
        public const string APP_JSON = "application/json";
    }


}