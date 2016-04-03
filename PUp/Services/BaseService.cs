using PUp.Models;
using PUp.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.ModelBinding;
namespace PUp.Services
{
    public class BaseService
    {
        protected RepositoryManager repo = new RepositoryManager();
        protected UserEntity currentUser=null;
        protected ModelStateWrapper modelStateWrapper;

        public BaseService(string email,  ModelStateWrapper modelStateWrapper)
        {   
            currentUser = repo.UserRepository.FindByEmail(email);
            this.modelStateWrapper  = modelStateWrapper ;
        }

        public RepositoryManager GetRepositoryManager() { return repo; }
        public UserEntity CurrentUser() { return  currentUser; }
    }
}