using PUp.Models.Entity;
using PUp.Models.SimpleObject;
using PUp.ViewModels.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PUp.Services
{
    public class ProjectService : BaseService
    {
        public ProjectService(string email) : base(email) { }

        public List<ProjectView> GetTableProjectForCurrentUser()
        {
            var projectsByUser = repo.ProjectRepository.GetAll()
                                     .Where(p => p.Owner == currentUser || p.Contributors.Contains(currentUser))
                                     .OrderByDescending(p => p.StartAt).ToList();
            var projectsView = new List<ProjectView>();
            projectsByUser.ForEach(p => projectsView.Add(new ProjectView(p)));
            return projectsView;
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

        public bool IsModelValid(AddProjectViewModel model)
        {
            if (model.EndAt <= model.StartAt || model.StartAt <= DateTime.Now.AddMinutes(30))
            {   /*
                modelStateWrapper.AddError("", "The form is not valid please check it again.");
                modelStateWrapper.Flash("Impossible to save the form in it's current state! ",FlashLevel.Danger);
                */
                return false;
            }
            return true;
        }
    }
}