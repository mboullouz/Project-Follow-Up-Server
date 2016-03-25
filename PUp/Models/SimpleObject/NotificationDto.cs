using PUp.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PUp.Models.SimpleObject
{
    public class NotificationDto
    {

        public int Id { get; set; }
        public string Url { get; set; }
        public string Message { get; set; }
        public bool Seen { get; set; }
        public int Level { get; set; }
        public UserDto User { get; set; }
        public DateTime AddAt { get; set; }
        public DateTime? EditAt { get; set; }
        public DateTime? DeleteAt { get; set; }
        public bool? Deleted { get; set; }


        public NotificationDto(NotificationEntity notif, int depth = AppConst.MaxDepth)
        {
            Init(notif, 1);
        }

        public void Init(NotificationEntity notif, int depth = AppConst.MaxDepth)
        {
            Id = notif.Id;
            Url = notif.Url;
            Message = notif.Message;
            Seen = notif.Seen;
            Level = notif.Level;
            User = new UserDto(notif.User, depth);
            AddAt = notif.AddAt;
            EditAt = notif.EditAt;
            DeleteAt = notif.DeleteAt;
            Deleted = notif.Deleted;

            TimeAgo = ComputeTimeAgo(notif.AddAt);
        }

        //Additional 
        public string TimeAgo { get; set; }

        public string ComputeTimeAgo(DateTime dateFrom)
        {
            var diff = DateTime.Now - dateFrom;
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
                return dateFrom.ToString("HH\\Hmm");
            }
            else
            {
                return AddAt.ToString("d-MM-yyyy HH\\Hmm");
            }

        }
    }


}