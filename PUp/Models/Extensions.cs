using PUp.Models.Entity;
using PUp.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PUp.Models
{
    public static class Extensions 
    {
        public static List< TaskDto> ToDto (this List<TaskEntity> source, int depth=AppConst.MaxDepth)  
        {
            var target = new List<TaskDto>();
            source.ForEach(s => target.Add(new TaskDto(s,depth)));
            return target;
        }

        public static List<IssueDto> ToDto(this List<IssueEntity> source, int depth = AppConst.MaxDepth)
        {
            var target = new List<IssueDto>();
            source.ForEach(s => target.Add(new IssueDto(s, depth)));
            return target;
        }

        public static List<NotificationDto> ToDto(this List<NotificationEntity> source, int depth = AppConst.MaxDepth)
        {
            var target = new List<NotificationDto>();
            source.ForEach(s => target.Add(new NotificationDto(s, depth)));
            return target;
        }

        public static string ComputeTimeAgo(this IBasicEntity entity)
        {
            var diff = DateTime.Now - entity.AddAt;
            if (diff.TotalMinutes < 1)
            {
                return ((int)diff.TotalSeconds) + " s ago";
            }
            else if (diff.TotalHours < 1)
            {
                return ((int)diff.TotalMinutes) + "min ago";
            }
            else if (diff.TotalDays < 1)
            {
                return entity.AddAt.ToString("HH\\Hmm");
            }
            else
            {
                return entity.AddAt.ToString("d-MM-yyyy HH\\Hmm");
            }

        }

    }
}