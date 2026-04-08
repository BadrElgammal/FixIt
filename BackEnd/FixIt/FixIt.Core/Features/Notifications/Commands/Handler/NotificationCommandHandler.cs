using AutoMapper;
using FixIt.Core.Bases;
using FixIt.Core.Features.Notifications.Commands.Models;
using FixIt.Domain.Entities;
using FixIt.Service.Abstracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Core.Features.Notifications.Commands.Handler
{
    public class NotificationCommandHandler : ResponseHandler,
                                IRequestHandler<AddNotificationCommand, Response<string>>,
                                IRequestHandler<MarkNotificationIsRead, Response<string>>
    {
        private readonly IMapper _mapper;
        private readonly INotificationService _notificationService;

        public NotificationCommandHandler(IMapper mapper, INotificationService notificationService)
        {
            _mapper = mapper;
            _notificationService = notificationService;
        }
        public async Task<Response<string>> Handle(AddNotificationCommand request, CancellationToken cancellationToken)
        {
            var NotificationMapping = _mapper.Map<Notification>(request);
            var result = await _notificationService.AddNotificationAsync(NotificationMapping);
            if(!result) return BadRequest<string>("فشل الاضافه الى الاشعارات");
            return Success("تم الاضافه الى الاشعارات");
        }

        public async Task<Response<string>> Handle(MarkNotificationIsRead request, CancellationToken cancellationToken)
        {
            var Notification = await _notificationService.GetNotificationById(request.NotificationId);
            if (Notification == null) return BadRequest<string>("لا يوجد اشعار");
            if(Notification.UserId != request.UserId) return BadRequest<string>("عفوا ليس لديك صلاحيه");

            Notification.IsRead = true;
            var result = await _notificationService.EditNotification(Notification);
            if(!result) return BadRequest<string>("خطأ فى تعديل حاله القرائه");
            return Success("تم تعديل حاله القراءه");

        }
    }
}
