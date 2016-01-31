using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PUp.Models.Entity
{
    public class NotificationEntity : IBasicEntity
    {
        [Key]
        public int Id { get; set; }
        public string Url { get; set; }

        [Column(TypeName = "ntext")]
        public string Message { get; set; }

        public bool Seen { get; set; }

        public UserEntity User { get; set; }

        public DateTime AddAt
        { get; set; }

        public DateTime? EditAt
        { get; set; }

        public DateTime? DeleteAt
        { get; set; }

        public bool? Deleted
        {
            get; set;
        }

        public NotificationEntity()
        {
            Seen = false;
            Url = "#";
            AddAt = DateTime.Now;
            Deleted = false;
            EditAt = AddAt;

        }
    }
}