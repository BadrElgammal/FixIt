using FixIt.Core.Features.Workers.Queries.DTOs;
using FixIt.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Core.Features.Workers.Queries.Models
{
    public class GetWorkerByIdQuery:IRequest<Bases.Response<GetSingleWorkerResponce>>//WorkerProfile
    {

        public Guid Id { get; set; }

        public GetWorkerByIdQuery(Guid id)
        {
            Id = id;
        }

    }
}
