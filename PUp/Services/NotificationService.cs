using PUp.Models;
using PUp.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PUp.Services
{

    public class NotificationService : BaseService
    {

        public NotificationService(string email, Models.ModelStateWrapper modelStateWrapper) : base(email, modelStateWrapper)
        { }

        public List<NotificationDto> AllForCurrentUser()
        {
            List<NotificationDto> notifs = new List<NotificationDto>();
            repo.NotificationRepository.GetByUser(currentUser)
                .OrderByDescending(n => n.AddAt).OrderByDescending(n=>!n.Seen)
                .Take(10).ToList()
                .ForEach(n => notifs.Add(new NotificationDto(n)));

            return notifs;
        }

        public ModelStateWrapper SeenUnseen(int notifId)
        {
            repo.NotificationRepository.SeenUnseen(notifId);
            modelStateWrapper.AddSuccess("Success", "Notification visibility changed");
            return modelStateWrapper;
        }
    }
}