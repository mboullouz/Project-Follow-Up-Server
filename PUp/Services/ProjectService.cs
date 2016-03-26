using PUp.Models;
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


        /// <summary>
        /// Prepare the DTOs for the client
        /// </summary>
        /// <returns></returns>
        public List<ProjectDto> GetTableProjectForCurrentUser()
        {
            var projectsByUser = repo.ProjectRepository.GetAll()
                                     .Where(p => p.Owner == currentUser || p.Contributors.Contains(currentUser))
                                     .OrderByDescending(p => p.StartAt).ToList();
            var projectsView = new List<ProjectDto>();
            projectsByUser.ForEach(p => projectsView.Add(new ProjectDto(p)));
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
            project.Owner = currentUser;
            project.Contributors.Add(currentUser);

            return project;
        }

        /// <summary>
        /// This is no more needed since the Client can generate it itself
        /// //TODO: remove
        /// </summary>
        /// <returns></returns>
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

        public ValidationMessageHolder CheckModel(AddProjectViewModel model,bool onEdit=false)
        {
            if (model.EndAt <= model.StartAt) {
                validationMessageHolder.Add("EndAt", "Date end must be superior to date start");
            }
            if ( model.StartAt <= DateTime.Now.AddMinutes(30))
            {
                validationMessageHolder.Add("StartAt", "Date start must be superior to actual date by at least 30 min");
            }
            if (onEdit)
            {
                if (model.Id <= 0)
                {
                    validationMessageHolder.Add("Id", "The Entity Id:" + model.Id + " is not valid");
                }
                if (model.Id > 0 && repo.ProjectRepository.FindById(model.Id) == null)
                {
                    validationMessageHolder.Add("Id", "Can't find Entity with the Id:" + model.Id);
                }
            }
            return validationMessageHolder;
        }
    }
}