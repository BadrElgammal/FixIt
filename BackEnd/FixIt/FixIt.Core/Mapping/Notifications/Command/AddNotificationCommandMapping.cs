using FixIt.Core.Features.Notifications.Commands.Models;
using FixIt.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Core.Mapping.Notifications
{
    public partial class NotificationMapping
    {
        public void AddNotificationCommandMapping()
        {
            CreateMap<AddNotificationCommand, Notification>();
        }
    }
}
