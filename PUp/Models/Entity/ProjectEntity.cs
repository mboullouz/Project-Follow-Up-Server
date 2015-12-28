using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PUp.Models.Entity
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public partial  class ProjectEntity
    {
         
        public ProjectEntity()
        {
            this.Tasks = new HashSet<TaskEntity>();
        }
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        
        public virtual ICollection<TaskEntity> Tasks { get; set; }
        public virtual UserEntity User { get; set; }
    }
}