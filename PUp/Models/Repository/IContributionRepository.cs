using PUp.Models.Entity;
using System.Collections.Generic;

namespace PUp.Models.Repository
{
    public interface IContributionRepository:IRepository<ContributionEntity>
    {
        List<ContributionEntity> GetByUser(UserEntity user);
        List<ContributionEntity> GetByProject(ProjectEntity project);
        List<ContributionEntity> GetByUserAndProject(UserEntity user, ProjectEntity project);
        bool ContributionExists(ProjectEntity project, UserEntity user);
        void RemoveAllForUser(UserEntity user);
        HashSet<UserEntity> UsersByProject(ProjectEntity p);
        void AddContributionIfNotExists(ProjectEntity project, UserEntity user, TaskEntity task);
    }
}
