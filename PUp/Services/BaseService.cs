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
        protected UserEntity currentUser=null;
        protected ValidationMessageHolder validationMessageHolder= new ValidationMessageHolder();

        public BaseService(string email)
        {   
            currentUser = repo.UserRepository.FindByEmail(email);

        }

        public RepositoryManager GetRepositoryManager() { return repo; }
        public UserEntity CurrentUser() { return  currentUser; }
    }
}