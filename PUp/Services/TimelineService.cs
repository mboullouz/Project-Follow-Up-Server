using PUp.Models.Entity;
using PUp.ViewModels.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.ModelBinding;

namespace PUp.Services
{
    public class TimelineService: BaseService
    {
        
        public TimelineService(string email, Models.ModelStateWrapper modelStateWrapper) : base(email, modelStateWrapper)
        {
        }

        public TimelineViewModel GetByProject(int id)
        {
            ProjectEntity project = repo.ProjectRepository.FindById(id);
            return new TimelineViewModel(project);
        }
    }
}