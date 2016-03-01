using PUp.Models;
using PUp.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PUp.Services
{
    public class BaseService
    {
        protected RepositoryManager repo = new RepositoryManager();
        protected UserEntity        currentUser = null;
        protected ModelStateWrapper modelStateWrapper;

        public BaseService(ModelStateWrapper modelStateWrapper)
        {
            currentUser = repo.UserRepository.GetCurrentUser();
            this.modelStateWrapper = modelStateWrapper;
        }

        public RepositoryManager GetRepositoryManager() { return repo; }
    }
}