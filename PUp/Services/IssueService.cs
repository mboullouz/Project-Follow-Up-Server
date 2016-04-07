using PUp.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PUp.Models;
using PUp.Models.Entity;

namespace PUp.Services
{
    public class IssueService:BaseService
    { 
        public IssueService(string email, Models.ModelStateWrapper modelStateWrapper) : base(email, modelStateWrapper) { }

        public  IssueDto GetIssue(int id)
        {
            var issue = repo.IssueRepository.FindById(id);
            return new IssueDto(issue, 1);
        }

        public  List<IssueDto> GetByProject(int id)
        {
            var p = repo.ProjectRepository.FindById(id);
            return  repo.IssueRepository.GetByProject(p).ToList().ToDto();
            
        }
        public ModelStateWrapper CheckModel(ViewModels.AddIssueViewModel model, bool onEdit = false)
        {
            if (model.ProjectId <= 0 || repo.ProjectRepository.FindById(model.ProjectId) == null)
            {
                modelStateWrapper.AddError("ProjectId", "The Entity ProjectId:" + model.Id + " is not valid");
            }

            if (onEdit)
            {
                if (model.Id <= 0)
                {
                    modelStateWrapper.AddError("Id", "The Entity Issue with Id:" + model.Id + " is not valid");
                }
                if (model.Id > 0 && repo.TaskRepository.FindById(model.Id) == null)
                {
                    modelStateWrapper.AddError("Id", "Can't find Entity Task with the Id:" + model.Id);
                }
            }
            return modelStateWrapper;
        }

        public IssueDto Add(ViewModels.AddIssueViewModel model)
        {
            ProjectEntity project = repo.ProjectRepository.FindById(model.ProjectId);
            IssueEntity issue = new IssueEntity
            {
                AddAt = DateTime.Now,
                EditAt = DateTime.Now,
                Deleted = false,
                Project = project,
                Description = model.Description,
                RelatedArea = model.RelatedArea,
                Status = model.Status,
                Submitter = currentUser,
            };
            repo.IssueRepository.Add(issue);
            project.Issues.Add(issue);
            project.Contributors.Add(currentUser);//add on submitting an issues
            string message = "New Issue is declared for the project <" + project.Name + ">";
            repo.NotificationRepository.NotifyAllUserInProject(project, message, LevelFlag.Danger);
            return new IssueDto(issue, 1);
        }

        internal ModelStateWrapper MarkResolved(int id)
        {
            var issue = repo.IssueRepository.MarkResolved(id);
            var project = issue.Project;
            string message = " Issue: <" + issue.Description + "> is closed";
            repo.NotificationRepository.NotifyAllUserInProject(project, message, LevelFlag.Success);
            return modelStateWrapper;
        }
    }
}