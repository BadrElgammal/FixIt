using FixIt.Core.Bases;
using FixIt.Domain.Enum;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FixIt.Core.Features.Notifications.Commands.Models
{
    public class AddNotificationCommand : IRequest<Response<string>>
    {

        public AddNotificationCommand(Guid UserId, string title, string message, NotificationType notificationType, string relatedEntityId)
        {
            this.UserId = UserId;
            Title = title;
            Message = message;
            NotificationType = notificationType;
            RelatedEntityId = relatedEntityId;
        }

        [JsonIgnore]
        public int NotificationId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public NotificationType? NotificationType { get; set; } //Message or Order or System
        public string? RelatedEntityId { get; set; }
        public Guid UserId { get; set; }
    }
}
