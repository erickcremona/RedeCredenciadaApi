using RedeCredenciadaDomain.Notifications;
using System.Collections.Generic;

namespace RedeCredenciadaDomain.Interfaces.Service
{
    public interface INotification
    {
        bool HasNotification();
        List<Notification> GetNotifications();
        void Handle(Notification notification);
    }
}
