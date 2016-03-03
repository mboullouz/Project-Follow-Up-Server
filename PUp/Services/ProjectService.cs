using PUp.ViewModels.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PUp.Services
{   
    public class ProjectService: BaseService
    {
        public ProjectService(ModelStateWrapper modelStateWrapper) : base(modelStateWrapper) { }

        public TableProjectModelView GetTableProjectForCurrentUser()
        {
            var projectsByUser = repo.ProjectRepository.GetAll()
                                     .Where(p => p.Owner == currentUser || p.Contributors.Contains(currentUser))
                                     .OrderByDescending(p => p.StartAt).ToList();
            TableProjectModelView tableProject = new TableProjectModelView
            {
                CurrentUser = currentUser,
                Projects = projectsByUser,
                OtherProjects = repo.ProjectRepository.GetActive()
                                    .Where(p => !projectsByUser.Contains(p))
                                    .OrderByDescending(p => p.StartAt).ToList(),
            };
            return  tableProject ;
        }
    }
}