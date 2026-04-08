using AutoMapper;
using FixIt.Core.Bases;
using FixIt.Core.Features.Workers.Queries.DTOs;
using FixIt.Core.Features.Workers.Queries.Models;
using FixIt.Core.Wrapper;
using FixIt.Domain.Entities;
using FixIt.Service.Abstracts;
using MediatR;
using System.Linq.Expressions;

namespace FixIt.Core.Features.Workers.Queries.Handlers
{
    public class WorkerQueryHandler : ResponseHandler,
                 IRequestHandler<GetWorkerByUserIdQuery, Bases.Response<GetSingleWorkerResponce>>,
                 IRequestHandler<GetWorkerProfileByWorkerIdQuery, Bases.Response<WorkerProfileDTO>>,
                 IRequestHandler<GetWorkersPaginatedListQuery, PaginatedResult<GetWorkersPaginatedResponce>>
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

        //For me userId by Tocken
        public async Task<Bases.Response<GetSingleWorkerResponce>> Handle(GetWorkerByUserIdQuery request, CancellationToken cancellationToken)
        {

            var user = await _WorkerService.GetWorkerById(request.userId);
            if (user == null) return NotFound<GetSingleWorkerResponce>();
            var result = _mapper.Map<GetSingleWorkerResponce>(user);
            return Success(result);
        }


        //For another worker by workerId
        public async Task<Bases.Response<WorkerProfileDTO>> Handle(GetWorkerProfileByWorkerIdQuery request, CancellationToken cancellationToken)
        {

            var workerProfile = await _WorkerService.GetWorkerByWorkerId(request.WorkerId);
            if (workerProfile == null) return NotFound<WorkerProfileDTO>();


            var workerProfileMapper = _mapper.Map<WorkerProfileDTO>(workerProfile);
            workerProfileMapper.ReviewsCounter = workerProfile.Reviews.Count;

            return Success(workerProfileMapper);

        }

        public Task<PaginatedResult<GetWorkersPaginatedResponce>> Handle(GetWorkersPaginatedListQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<WorkerProfile, GetWorkersPaginatedResponce>> expression = e => new GetWorkersPaginatedResponce(e.WorkerId, e.JobTitle, e.Description, e.AvailabilityStatus, e.RatingAverage, e.Area, e.Category.CategoryName, e.User.FullName, e.User.ImgUrl, e.User.Role, e.User.City);
            var FilterQuery = _WorkerService.GetAllWorkersPaginatedWithFiltaration(request.search, request.address, request.IsAvilable);
            var paginatedList = FilterQuery.Select(expression).ToPaginatedListAsync(request.pageNum, request.pageSize);
            return paginatedList;
        }



        #endregion

    }
}
