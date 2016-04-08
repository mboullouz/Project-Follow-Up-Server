using PUp.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PUp.ViewModels.Project
{
    public class MatrixViewModel : BaseModelView
    {
        public ProjectEntity Project { get; set; }
        public UserEntity User { get; set; }
        public List<TaskEntity> ImportantAndUrgent { get; set; }
        public List<TaskEntity> ImportantNotUrgent { get; set; }
        public List<TaskEntity> NotImportantButUrgent { get; set; }
        public List<TaskEntity> NotImportantNotUrgent { get; set; }


        public MatrixViewModel(ProjectEntity project, UserEntity user)
        {
            Project = project;
            User = user;
            Init(project.Tasks);
        }
        public MatrixViewModel(ICollection<TaskEntity> tasks, UserEntity user)
        {
            User = user;
            Init(tasks);
        }

        public void Init(ICollection<TaskEntity> tasks)
        {

            ImportantAndUrgent = new List<TaskEntity>();
            ImportantNotUrgent = new List<TaskEntity>();
            NotImportantButUrgent = new List<TaskEntity>();
            NotImportantNotUrgent = new List<TaskEntity>();

            ImportantAndUrgent.AddRange(tasks.Where(t => t.Critical && t.Urgent && t.Executor == User));
            ImportantNotUrgent.AddRange(tasks.Where(t => t.Critical && !t.Urgent && t.Executor == User));
            NotImportantButUrgent.AddRange(tasks.Where(t => !t.Critical && t.Urgent && t.Executor == User));
            NotImportantNotUrgent.AddRange(tasks.Where(t => !t.Critical && !t.Urgent && t.Executor == User));
        }
    }
}