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
        public TimelineService(string email) : base(email)
        {

        }

        public TimelineService(string email, ModelStateDictionary modelState) : base(email, modelState)
        {
        }

        public TimelineViewModel GetByProject(int id)
        {
            ProjectEntity project = repo.ProjectRepository.FindById(id);
            return new TimelineViewModel(project);
        }
    }
}