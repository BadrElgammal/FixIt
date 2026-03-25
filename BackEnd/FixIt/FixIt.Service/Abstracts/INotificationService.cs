using FixIt.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Service.Abstracts
{
    public interface INotificationService
    { 
        Task<bool> AddNotificationAsync(Notification notification);
        Task<List<Notification>> GetAllNotificationsAsync(Guid userId);
    }
}
