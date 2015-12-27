using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PUp.Models.Entity
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public partial  class Project
    {
         
        public Project()
        {
            this.Tasks = new HashSet<Task>();
        }
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        
        public virtual ICollection<Task> Tasks { get; set; }
        public virtual User User { get; set; }
    }
}