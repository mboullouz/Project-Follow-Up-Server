using PUp.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PUp.Models.Repository
{
    interface IContributionRepository:IRepository<ContributionEntity>
    {
        List<ContributionEntity> GetByUser(UserEntity user);
        List<ContributionEntity> GetByProject(ProjectEntity project);
        List<ContributionEntity> GetByUserAndProject(UserEntity user, ProjectEntity project);
    }
}
