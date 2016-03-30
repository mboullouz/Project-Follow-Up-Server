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
        protected ValidationMessageHolder validationMessageHolder= new ValidationMessageHolder();
        protected  ModelStateDictionary modelState;

        public BaseService(string email,  ModelStateDictionary modelState)
        {   
            currentUser = repo.UserRepository.FindByEmail(email);
            this.modelState  = modelState ;
        }

        public RepositoryManager GetRepositoryManager() { return repo; }
        public UserEntity CurrentUser() { return  currentUser; }
    }
}