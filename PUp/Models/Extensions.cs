using PUp.Models.SimpleObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PUp.Models
{
    public static class Extensions 
    {
        public static List< TaskDto> ToDto (this List<Entity.TaskEntity> source)  
        {
            var target = new List<TaskDto>();
            source.ForEach(s => target.Add(new TaskDto(s)));
            return target;
        }
    }
}