using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PUp.Models.Entity
{
    using Microsoft.AspNet.Identity;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Security.Claims;
    public partial class UserEntity : Microsoft.AspNet.Identity.EntityFramework.IdentityUser
    {


        public UserEntity()
        {
            this.Contributions = new HashSet<ContributionEntity>();
        }
        public virtual ICollection<ContributionEntity> Contributions { get; set; }
        public async System.Threading.Tasks.Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<UserEntity> manager)
        {
            // Notez qu'authenticationType doit correspondre à l'élément défini dans CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Ajouter les revendications personnalisées de l’utilisateur ici
            return userIdentity;
        }
    }
}