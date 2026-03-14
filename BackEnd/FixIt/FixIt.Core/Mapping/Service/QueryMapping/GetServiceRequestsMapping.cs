using FixIt.Core.Features.Service.Commands.DTOs;
using FixIt.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Core.Mapping.Service
{
    public partial class ServiceMapping
    {
        public void GetAllServiceRequests()
        {
            CreateMap<ServiceRequest, ServiceRequestDTO>();
        }
    }
}
