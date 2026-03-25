using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Core.Mapping.Notifications
{
    public partial class NotificationMapping : Profile
    {
        public NotificationMapping()
        {
            AddNotificationCommandMapping();
        }
    }
}
