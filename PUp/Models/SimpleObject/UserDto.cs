﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PUp.Models.Entity;

namespace PUp.Models.SimpleObject
{
    public class UserDto
    {
       

        public UserDto(UserEntity u)
        {
            Init(u);
        }

        public void Init(UserEntity u)
        {
            if (u != null)
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
    }
}