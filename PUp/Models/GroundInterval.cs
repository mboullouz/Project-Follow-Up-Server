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
            for (var hourKey = AppConst.DayStart; hourKey <= AppConst.DayEnd; hourKey++)
            {
                Interval.Add(new KeyValuePair<int, bool>(hourKey, false));
            }
        }

        public void MakeNotAvelaibleHoursBeforeNow()
        {
            int actualHour = DateTime.Now.Hour;
            for (var hourKey = AppConst.DayStart; hourKey <= actualHour; hourKey++)
            {
                Interval[hourKey] = true;
            }
        }

        //TODO: Prevent add when current datetime.hour>AppConst.DayEnd
        //TODO USE CheckForDate before start adding!
        public IDictionary<int, bool> AddDate(DateTime startDate, int durationInHours)
        {
            int startH = startDate.Hour;
            MakeNotAvelaibleHoursBeforeNow();
            if (startH < AppConst.DayStart || startH > AppConst.DayEnd)
            {
                return Interval;
            }
            for (var h = startH; h < startH + durationInHours; h++)
            {
                if(!Interval[h])
                    Interval[h] = true;
            }
            return Interval;
        }

        public bool CheckForDateAndDuration(DateTime startDate, int duration)
        {
            MakeNotAvelaibleHoursBeforeNow();
            if (IsFull())
            {
                return false;
            }
            int startH = startDate.Hour;
            if (Interval[startH] || startH + duration > AppConst.DayEnd | startH < DateTime.Now.Hour)
            {
                return false;
            }
            for (int i = startH; i < startH + duration; i++)
            {
                if (Interval[i])
                {
                    return false;
                }
            }
            return true;
        }

        public bool IsFull()
        {
            return Interval.Count(v => v.Value == true) == Interval.Count();
        }
    }
}