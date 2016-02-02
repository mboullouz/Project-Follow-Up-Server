using PUp.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PUp.ViewModels.Project
{
    public class MatrixViewModel
    {   
        public List<TaskEntity> ImportantAndUrgent { get; set; }
        public List<TaskEntity> ImportantNotUrgent { get; set; }
        public List<TaskEntity> NotImportantButUrgent { get; set; }
        public List<TaskEntity> NotImportantNotUrgent { get; set; }
        public MatrixViewModel(ProjectEntity project)
        {
            Init(project);
        }
        public void Init(ProjectEntity project)
        {
            var tasks = project.Tasks;
            ImportantAndUrgent = new List<TaskEntity>();
            ImportantNotUrgent = new List<TaskEntity>();
            NotImportantButUrgent = new List<TaskEntity>();
            NotImportantNotUrgent = new List<TaskEntity>();

            ImportantAndUrgent.AddRange(tasks.Where(t => t.Important && t.Urgent));
            ImportantNotUrgent.AddRange(tasks.Where(t => t.Important && !t.Urgent));
            NotImportantButUrgent.AddRange(tasks.Where(t => !t.Important && t.Urgent));
            NotImportantNotUrgent.AddRange(tasks.Where(t => !t.Important && !t.Urgent));
        }
    }
}