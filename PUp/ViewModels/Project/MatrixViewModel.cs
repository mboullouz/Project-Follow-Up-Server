using PUp.Models.Dto;
using PUp.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PUp.ViewModels.Project
{
    public class MatrixViewModel : BaseModelView
    {
        public ProjectDto Project { get; set; }
        public UserDto User { get; set; }
        public List<TaskDto> ImportantAndUrgent { get; set; }
        public List<TaskDto> ImportantNotUrgent { get; set; }
        public List<TaskDto> NotImportantButUrgent { get; set; }
        public List<TaskDto> NotImportantNotUrgent { get; set; }


        public MatrixViewModel(ProjectDto project, UserDto user)
        {
            Project = project;
            User = user;
            Init(project.Tasks);
        }
        public MatrixViewModel(ICollection<TaskDto> tasks, UserDto user)
        {
            User = user;
            Init(tasks);
        }

        public void Init(ICollection<TaskDto> tasks)
        {
            ImportantAndUrgent = new List<TaskDto>();
            ImportantNotUrgent = new List<TaskDto>();
            NotImportantButUrgent = new List<TaskDto>();
            NotImportantNotUrgent = new List<TaskDto>();

            ImportantAndUrgent.AddRange(tasks.Where(t => t.Critical && t.Urgent && t.Executor == User));
            ImportantNotUrgent.AddRange(tasks.Where(t => t.Critical && !t.Urgent && t.Executor == User));
            NotImportantButUrgent.AddRange(tasks.Where(t => !t.Critical && t.Urgent && t.Executor == User));
            NotImportantNotUrgent.AddRange(tasks.Where(t => !t.Critical && !t.Urgent && t.Executor == User));
        }
    }
}