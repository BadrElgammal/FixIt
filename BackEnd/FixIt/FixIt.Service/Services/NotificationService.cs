using FixIt.Domain.Entities;
using FixIt.Infrastructure.Abstracts;
using FixIt.Service.Abstracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Service.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IGenericRepositoryAsync<Notification> _notificationRepo;

        public NotificationService(IGenericRepositoryAsync<Notification> notificationRepo)
        {
            _notificationRepo = notificationRepo;
        }
        public async Task<bool> AddNotificationAsync(Notification notification)
        {
            await _notificationRepo.AddAsync(notification);
            return true;
        }

        public async Task<bool> EditNotification(Notification notification)
        {
             await _notificationRepo.UpdateAsync(notification);
            return true;
        }

        public async Task<List<Notification>> GetAllNotificationsAsync(Guid userId)
        {
            return await _notificationRepo.Find(n => n.UserId == userId).ToListAsync();
        }

        public async Task<Notification> GetNotificationById(int notificationId)
        {
            return await _notificationRepo.GetByIdAsync(notificationId);
        }
    }
}
