using PUp.Models.Entity;
using PUp.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PUp.Models.Repository
{
    public interface ITaskRepository:IRepository<TaskEntity>
    {   
        
        /// <summary>
        /// Mark a Task Done: value=true or Pending: value =false;
        /// </summary>
        /// <param name="value"></param>
        void ChangeTaskState(int id, bool value);
    }
}
