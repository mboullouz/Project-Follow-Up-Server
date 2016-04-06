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

        /// <summary>
        /// Group Repositories in one simple class, sometimes one service may need to
        /// intercat with several repositories
        /// </summary>
        protected RepositoryManager repo = new RepositoryManager();

        /// <summary>
        /// By default current user is set to null, each request (RequestContext) that passes througth Authentification
        /// is initialized with an email
        /// @see App_Start.MyBasicAuth
        /// </summary>
        protected UserEntity currentUser=null;


        /// <summary>
        /// The ModelState is part of ApiController, the additional MessageHolder is a simple POCO
        /// to hold 1-messages about validations  and State of validation then send it to service caller
        /// </summary>
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