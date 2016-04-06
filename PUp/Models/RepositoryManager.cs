using PUp.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PUp.Models
{   

    /// <summary>
    /// Just a simple holder of all repositories int the app to make them available as one class
    /// and initialized with the same Db context
    /// </summary>
    public class RepositoryManager
    {
        public TaskRepository TaskRepository {  get; }
        public ProjectRepository ProjectRepository { get; }
        public UserRepository UserRepository { get; }
        public NotificationRepository NotificationRepository { get; }
        public IssueRepository IssueRepository { get; }
        public DatabaseContext DbContext { get; set; }

        public RepositoryManager()
        {
            DbContext = new DatabaseContext();
            TaskRepository = new TaskRepository(DbContext);
            ProjectRepository = new ProjectRepository(DbContext);
            UserRepository = new UserRepository(DbContext);
            NotificationRepository = new NotificationRepository(DbContext);
            IssueRepository = new IssueRepository(DbContext);
        }
         
    }
}