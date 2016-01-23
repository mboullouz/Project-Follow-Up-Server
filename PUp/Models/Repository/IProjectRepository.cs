using PUp.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PUp.Models.Repository
{
    public interface IProjectRepository:IRepository<ProjectEntity>
    {
        List<ProjectEntity> GetByUser(UserEntity user,bool isDeleted=false);
        void Remove(int id);
        void SoftRemove(int id);
        List<ProjectEntity> GetActive();
    }
}
