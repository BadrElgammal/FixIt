using AutoMapper;
using FixIt.Core.Bases;
using FixIt.Core.Features.Workers.Queries.DTOs;
using FixIt.Core.Features.Workers.Queries.Models;
using FixIt.Core.Features.Workers.Queries.Results;
using FixIt.Service.Abstracts;
using MediatR;

namespace FixIt.Core.Features.Workers.Queries.Handlers
{
    public class WorkerQueryHandler : ResponseHandler,
                 IRequestHandler<GetWorkersListQuery, Bases.Response<List<GetWorkersResponce>>>, // WorkerProfile
                 IRequestHandler<GetWorkerByIdQuery, Bases.Response<GetSingleWorkerResponce>>,
                 IRequestHandler<GetWorkerProfileByWorkerIdQuery, Bases.Response<WorkerProfileDTO>>
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

        public async Task<Bases.Response<WorkerProfileDTO>> Handle(GetWorkerProfileByWorkerIdQuery request, CancellationToken cancellationToken)
        {

            var workerProfile = await _WorkerService.GetWorkerByWorkerId(request.WorkerId);
            if (workerProfile == null) return NotFound<WorkerProfileDTO>();

            var workerProfileMapper = _mapper.Map<WorkerProfileDTO>(workerProfile);
            return Success(workerProfileMapper);

        }



        #endregion

    }
}
