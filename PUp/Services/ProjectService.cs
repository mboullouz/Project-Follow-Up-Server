using PUp.Models.Entity;
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

        public ProjectEntity GetInitializedProjectFromModel(AddProjectViewModel model)
        {
            ProjectEntity project = new ProjectEntity();
            project.Name = model.Name;
            project.StartAt = model.StartAt;
            project.EndAt = model.EndAt;
            project.Objective = model.Objective;
            project.Benifite = model.Benifite;
            project.Owner = repo.UserRepository.GetCurrentUser();
            project.Contributors.Add(project.Owner);
            
            return project;
        }

        public AddProjectViewModel InitProjectModel()
        {
            AddProjectViewModel projectModel = new AddProjectViewModel
            {
                EndAt = DateTime.Now.AddDays(7),
                StartAt = DateTime.Now.AddHours(1),
                Name = "Week 2: Exciting project!",
                Benifite = "Define the benefits and related assumptions,associated with the project...",
                Objective = "what is the project trying to achieve? What functionalities or departments are involved?... "
            };
            return projectModel;
        }
    }
}