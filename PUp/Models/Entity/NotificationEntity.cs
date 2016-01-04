using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PUp.Models.Entity
{
    public class NotificationEntity
    {
        [Key]
        public int Id { get; set; }
        public string Url { get; set; }
        [Column(TypeName = "ntext")]
        public string Message { get; set; }
        public bool Seen { get; set; }
        public DateTime CreateAt { get; set; }
        public UserEntity User { get; set; }

        public NotificationEntity()
        {
            Seen = false;
            Url = "#";
            CreateAt = DateTime.Now;

        }
    }
}