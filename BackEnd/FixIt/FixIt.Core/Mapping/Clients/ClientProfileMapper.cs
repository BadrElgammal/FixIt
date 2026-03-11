using AutoMapper;
using FixIt.Core.Features.Clients.Queries.DTOs;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Core.Mapping.Clients
{
    public partial class ClientProfileMapper:Profile
    {
        public ClientProfileMapper()
        {
            GetClientProfile();
            EditClient();
        }
    }
}
