using PUp.Models.SimpleObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PUp.Models
{
    public static class Extensions 
    {
        public static List< TaskDto> ToDto (this List<Entity.TaskEntity> source, int depth=AppConst.MaxDepth)  
        {
            var target = new List<TaskDto>();
            source.ForEach(s => target.Add(new TaskDto(s,depth)));
            return target;
        }

        public static List<IssueDto> ToDto(this List<Entity.IssueEntity> source, int depth = AppConst.MaxDepth)
        {
            var target = new List<IssueDto>();
            source.ForEach(s => target.Add(new IssueDto(s, depth)));
            return target;
        }

        public static List<NotificationDto> ToDto(this List<Entity.NotificationEntity> source, int depth = AppConst.MaxDepth)
        {
            var target = new List<NotificationDto>();
            source.ForEach(s => target.Add(new NotificationDto(s, depth)));
            return target;
        }
    }
}