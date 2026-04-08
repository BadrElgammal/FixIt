using FixIt.Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Core.Features.Notifications.Commands.Models
{
    public class MarkNotificationIsRead : IRequest<Response<string>>
    {
        public int NotificationId { get; set; }
        public Guid UserId { get; set; }
        public MarkNotificationIsRead( int  notificationId , Guid userId)
        {
            NotificationId = notificationId;
            UserId = userId;
        }

    }
}
