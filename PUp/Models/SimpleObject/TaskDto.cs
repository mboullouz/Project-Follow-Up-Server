using PUp.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PUp.Models.SimpleObject
{
    public class TaskDto : BaseDto,IBasicEntity
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
        public ProjectDto Project { set; get; }

        public DateTime? EditAt { get; set; }
        

        public TaskDto(TaskEntity t, int depth = AppConst.MaxDepth)
        {
            Init(t,--depth);
        }

        public void Init(TaskEntity t,int depth)
        {
            if (t != null && depth > 0)
            {
                Id = t.Id;
                Title = t.Title;
                Description = t.Description;
                Done = t.Done;
                EstimatedTimeInMinutes = t.EstimatedTimeInMinutes;
                Urgent = t.Urgent;
                Critical = t.Critical;
                Postponed = t.Postponed;
                AddAt = t.AddAt;
                FinishAt = t.FinishAt;
                KeyFactor = t.KeyFactor;
                StartAt = t.StartAt;
                EndAt = t.EndAt;
                DeleteAt = t.DeleteAt;
                Deleted = t.Deleted;
                Project = new ProjectDto(t.Project, 1);
                Executor = new UserDto(t.Executor,depth);
                TimeAgo = this.ComputeTimeAgo();
            }
        }

        //Additional 
        public string Type = "Task";
        public string TimeAgo { get; set; }
    }
}