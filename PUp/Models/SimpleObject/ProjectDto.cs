using PUp.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PUp.Models.SimpleObject
{
    public class ProjectDto : IBasicEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Finish { get; set; }
        public bool? Deleted { get; set; }
        public DateTime? EditAt { get; set; }
        public DateTime FinishedAt { get; set; }
        public DateTime StartAt { get; set; }
        public UserDto Owner { get; set; }
        public ICollection<UserDto> Contributors { get; set; }
        public DateTime EndAt { get; set; }
        public string Benifite { get; set; }
        public string Objective { get; set; }
        public virtual ICollection<TaskDto> Tasks { get; set; }
        public virtual ICollection<IssueDto> Issues { get; set; }
        public DateTime? DeleteAt { get; set; }
        public DateTime AddAt { get; set; }


        public ProjectDto(ProjectEntity project, int depth = AppConst.MaxDepth)
        {
            Init(project, --depth);
        }



        public void Init(ProjectEntity project, int depth= AppConst.MaxDepth)
        {   
            if(project!=null && depth > 0)
            {
                Contributors = new HashSet<UserDto>();
                Tasks = new HashSet<TaskDto>();
                Issues = new HashSet<IssueDto>();

                Id = project.Id;
                Name = project.Name;
                Finish = project.Finish;
                Owner = new UserDto(project.Owner, depth);
                Benifite = project.Benifite;
                Objective = project.Objective;
                EndAt = project.EndAt;
                EditAt = project.EditAt;
                FinishedAt = project.FinishedAt;
                AddAt = project.AddAt;
                DeleteAt = project.DeleteAt;

                project.Contributors.ToList().ForEach(u => Contributors.Add(new UserDto(u, depth)));
                project.Tasks.ToList().ForEach(t => Tasks.Add(new TaskDto(t, depth)));
                project.Issues.ToList().ForEach(i => Issues.Add(new IssueDto(i, depth)));

                InitAdditional(project);
            }
            
        }

        
        /// <summary>
        /// Additional 'computation' to use instead of re-compute by the JS
        /// </summary>
        public int Progress { get; set; }
        public int TasksDone { get; set; }
        public int TotalTasks { get; set; }
        public bool Over { get; set; }
        public int TotalIssues { get; set; }

        public void InitAdditional(ProjectEntity p)
        {
            TotalTasks = p.Tasks.Count();
            TasksDone = p.Tasks.Where(t => t.Done == true).Count();
            Progress = (int)(TasksDone / (TotalTasks + 0.1) * 100);
            Over = p.EndAt < DateTime.Now;
        }

    }
}