using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PUp.Models
{
    public class GroundInterval
    {
        public IDictionary<int, bool> Interval { get; set; }

        public GroundInterval()
        {
            Interval = new Dictionary<int, bool>();
            Init();
        }

        public void Init()
        {
            for(var hourKey= AppConst.DayStart; hourKey <= AppConst.DayEnd; hourKey++)
            {
               Interval.Add(new KeyValuePair<int, bool>(hourKey, false));
            }
        }

        public IDictionary<int, bool> AddDate(DateTime startDate,int durationInHours)
        {   
            int startH = startDate.Hour;
            
            if(startH < AppConst.DayStart || startH > AppConst.DayEnd)
            {
                return Interval;
            } 
            for (var h = startH; h < startH+durationInHours; h++)
            {
                Interval[h] = true;
            }
            return Interval; 
        }

        public bool IsFull()
        {
            return Interval.Count(v => v.Value == true) == Interval.Count();
        }
    }
}