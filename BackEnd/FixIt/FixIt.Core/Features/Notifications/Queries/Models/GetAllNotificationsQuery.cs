using FixIt.Core.Bases;
using FixIt.Core.Features.Notifications.Queries.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Core.Features.Notifications.Queries.Models
{
    public class GetAllNotificationsQuery : IRequest<Response<List<NotificationQueryDTO>>>
    {
        public Guid UserId { get; set; }
        public GetAllNotificationsQuery(Guid userId)
        {
            UserId = userId;
        }
    }
}
