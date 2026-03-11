using FixIt.Core.Features.Clients.Queries.DTOs;
using FixIt.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Core.Mapping.Clients
{
    public partial class ClientProfileMapper
    {
        public void GetClientProfile()
        {
            CreateMap<User, ClientProfileDTO>();
        }
    }
}
