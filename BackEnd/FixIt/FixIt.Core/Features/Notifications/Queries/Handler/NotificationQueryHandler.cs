using AutoMapper;
using FixIt.Core.Bases;
using FixIt.Core.Features.Notifications.Queries.DTOs;
using FixIt.Core.Features.Notifications.Queries.Models;
using FixIt.Service.Abstracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Core.Features.Notifications.Queries.Handler
{
    public class NotificationQueryHandler : ResponseHandler,
                            IRequestHandler<GetAllNotificationsQuery, Response<List<NotificationQueryDTO>>>
    {
        private readonly IMapper _mapper;
        private readonly INotificationService _notificationService;

        public NotificationQueryHandler(IMapper mapper, INotificationService notificationService)
        {
            _mapper = mapper;
            _notificationService = notificationService;
        }
        public async Task<Response<List<NotificationQueryDTO>>> Handle(GetAllNotificationsQuery request, CancellationToken cancellationToken)
        {
            var Notifications = await _notificationService.GetAllNotificationsAsync(request.UserId);
            if(!Notifications.Any()) return NotFound<List<NotificationQueryDTO>>("لا يوجد اشعارات"); 

            var NotificationsMapper = _mapper.Map<List<NotificationQueryDTO>>(Notifications);
            return Success(NotificationsMapper);
        }
    }
}
