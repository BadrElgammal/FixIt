using AutoMapper;
using Azure;
using FixIt.Core.Bases;
using FixIt.Core.Features.Workers.Queries.DTOs;
using FixIt.Core.Features.Workers.Queries.Models;
using FixIt.Core.Features.Workers.Queries.Results;
using FixIt.Domain.Entities;
using FixIt.Service.Abstracts;
using FixIt.Service.Services;
using MediatR;
using Microsoft.Azure.Documents.SystemFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Core.Features.Workers.Queries.Handlers
{
    public class WorkerQueryHandler : ResponseHandler, 
                 IRequestHandler<GetWorkersListQuery, Bases.Response<List<GetWorkersResponce>>>, // WorkerProfile
                 IRequestHandler<GetWorkerByIdQuery, Bases.Response<GetSingleWorkerResponce>>
    {

        #region Fields

        private readonly IWorkerService _WorkerService;
        private readonly IMapper _mapper;

        #endregion

        //Ctors

        #region Ctors

        public WorkerQueryHandler(IWorkerService WorkerService, IMapper mapper)
        {
            _WorkerService = WorkerService;
            _mapper = mapper;
        }

        #endregion

        //
        #region methods handel
        public async Task<Bases.Response<List<GetWorkersResponce>>> Handle(GetWorkersListQuery request, CancellationToken cancellationToken)
        {
            var WorkersList = await _WorkerService.GetAllWorkersAsync();
            var WorkersListMapper = _mapper.Map<List<GetWorkersResponce>>(WorkersList);
            return Success(WorkersListMapper);
        }


        public async Task<Bases.Response<GetSingleWorkerResponce>> Handle(GetWorkerByIdQuery request, CancellationToken cancellationToken)
        {

            var user = await _WorkerService.GetWorkerById(request.Id);
            if (user == null) return NotFound<GetSingleWorkerResponce>();
            var result = _mapper.Map<GetSingleWorkerResponce>(user);
            return Success(result);
        }



        #endregion

    }
}
