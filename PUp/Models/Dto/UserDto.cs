using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PUp.Models.Entity;

namespace PUp.Models.Dto
{
    public class UserDto:BaseDto
    {
       

        public UserDto(UserEntity u, int depth = AppConst.MaxDepth)
        {
            Init(u,--depth);
        }

        public void Init(UserEntity u,int depth)
        {
            if (u != null && depth>0)
            {
                Name = u.Name;
                Email = u.Email;
                Id = u.Id;
                UserName = u.UserName;
            }
           
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Id { get; set; }
        public string UserName { get; set; }

        public ICollection<ProjectDto> Projects { get; set; }
        public ICollection<TaskDto> Tasks { get; set; }

        //Additional 
        public string Type = "User";
    }
}