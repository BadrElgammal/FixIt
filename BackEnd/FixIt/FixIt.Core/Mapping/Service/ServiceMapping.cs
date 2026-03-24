using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Core.Mapping.Service
{
    public partial class ServiceMapping : Profile
    {
        public ServiceMapping()
        {
            CreateServieRequestMapping();
            GetAllServiceRequists();
            GetServiceRequest();
        }
    }
}
