using AutoMapper;

namespace FixIt.Core.Mapping.Notifications
{
    public partial class NotificationMapping : Profile
    {
        public NotificationMapping()
        {
            AddNotificationCommandMapping();
            GetAllNotificationsQueryMapping();
        }
    }
}
