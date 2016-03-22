using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PUp.Models.SimpleObject
{
    public class UserDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Id { get; set; }

        public ICollection<ProjectDto> Projects { get; set; }
        public ICollection<TaskDto> Tasks { get; set; }
    }
}