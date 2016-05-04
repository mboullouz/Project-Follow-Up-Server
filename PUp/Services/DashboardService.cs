using PUp.ViewModels;
using PUp.ViewModels.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PUp.Models;
using PUp.Models.Dto;
using PUp.ViewModels.Dashboard;

namespace PUp.Services
{
    public class DashboardService: BaseService
    {
        public DashboardService(string email,  ModelStateWrapper modelStateWrapper) : base(email, modelStateWrapper) { }

        public DashboardModelView GetInitialisedDashboard()
        {
            DashboardModelView dashboardMV = new DashboardModelView();
            var currentTasks = repo.TaskRepository.TodayTasksByUser(currentUser).Where(t => t.Done == false).ToList().ToDto();
            var doneToday = repo.TaskRepository.TodayTasksByUser(currentUser).Where(t => t.Done == true).ToList().ToDto();
            dashboardMV.CurrentTasks = currentTasks;
            var otherTasks = repo.TaskRepository.GetAll()
                                           .Where(t => t.Executor == currentUser && !t.Done && t.StartAt == null && t.Project.EndAt > DateTime.Now && t.Project.Deleted == false)
                                           .ToList().ToDto();
            dashboardMV.MatrixVM = new MatrixViewModel(currentTasks, new UserDto(currentUser,2));
            dashboardMV.OtherTasks = otherTasks;
            dashboardMV.TodayDoneTasks = doneToday;
            return dashboardMV;
        }
    }

    
}