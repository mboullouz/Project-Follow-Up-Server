using PUp.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PUp.Models.SimpleObject
{
    public class ProjectDto
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

        public ProjectDto(ProjectEntity project, int depth=2)
        {
            Contributors = new HashSet<UserDto>();
            Tasks = new HashSet<TaskDto>();
            Issues = new HashSet<IssueDto>();

            Id = project.Id;
            Name = project.Name;
            Finish = project.Finish;
            Owner = new UserDto(project.Owner,depth);
            Benifite = project.Benifite;
            Objective = project.Objective;
            EndAt = project.EndAt;
            EditAt = project.EditAt;
            FinishedAt = project.FinishedAt;
            AddAt = project.AddAt;
            DeleteAt = project.DeleteAt;
           
            project.Contributors.ToList().ForEach(u => Contributors.Add(new UserDto(u,depth)));
            project.Tasks.ToList().ForEach(t => Tasks.Add(new TaskDto(t,depth)));
            /*int nTasks = project.Tasks.Count;
            for (int i=0;i<nTasks;i++)
            {
                Tasks.Add(new TaskDto(project.Tasks.ElementAtOrDefault(i)) );
            }*/
             
            project.Issues.ToList().ForEach(i => Issues.Add(new IssueDto(i,depth)));
        }
    }
}