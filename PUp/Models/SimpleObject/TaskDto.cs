using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PUp.Models.SimpleObject
{
    public class TaskDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Done { get; set; }
        public int EstimatedTimeInMinutes { get; set; }
        public bool Urgent { get; set; }
        public bool Critical { get; set; }
        public bool Postponed { get; set; }
        public DateTime AddAt { get; set; }
        public Nullable<DateTime> FinishAt { get; set; }
        public bool KeyFactor { get; set; }
        public DateTime? StartAt { get; set; }
        public DateTime? EndAt { get; set; }
        public DateTime? DeleteAt { get; set; }
        public bool? Deleted { get; set; }
        public UserDto Executor { set; get; }
    }
}