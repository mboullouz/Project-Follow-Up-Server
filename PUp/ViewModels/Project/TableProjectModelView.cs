using PUp.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PUp.ViewModels.Project
{
    public class TableProjectModelView
    {
        public List<ProjectEntity> Projects { get; set; }
        public UserEntity CurrentUser { get; set; }

        public int CountDone(ProjectEntity p)
        {
            return p.Tasks.Where(t => t.Done == true).Count();
        }
        public bool IsFinish(ProjectEntity p)
        {
            if (p.EndAt <= DateTime.Now)
            {
                return true;
            }
             
            return false;
        }
    }
}