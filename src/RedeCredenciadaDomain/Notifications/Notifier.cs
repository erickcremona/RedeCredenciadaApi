using RedeCredenciadaDomain.Interfaces.Service;
using System.Collections.Generic;
using System.Linq;

namespace RedeCredenciadaDomain.Notifications
{
    public class Notifier : INotification
    {
        private List<Notification> _notifications;

        public Notifier()
        => _notifications = new List<Notification>();

        public void Handle(Notification notification)
                 => _notifications.Add(notification);

        public List<Notification> GetNotifications()
                                  => _notifications;

        public bool HasNotification()
             => _notifications.Any();

    }
}
