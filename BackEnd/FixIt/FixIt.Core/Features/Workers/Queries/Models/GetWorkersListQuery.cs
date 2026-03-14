using FixIt.Core.Bases;
using FixIt.Core.Features.Workers.Queries.Results;
using FixIt.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Core.Features.Workers.Queries.Models
{
    public class GetWorkersListQuery :IRequest<Response<List<GetWorkersResponce>>>//WorkerProfile
    {



    }
}
