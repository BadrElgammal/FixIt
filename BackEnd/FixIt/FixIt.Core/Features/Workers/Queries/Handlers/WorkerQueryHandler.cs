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
                 IRequestHandler<GetWorkersPaginatedListQuery, PaginatedResult<GetWorkersPaginatedResponce>>,
                 IRequestHandler<GetLastServicesRequestByUserIdQuery, Bases.Response<List<ServiceDTO>>>,
                 IRequestHandler<GetLastReviewsQuery, Bases.Response<List<LastReviewDTO>>>,
                 IRequestHandler<GetLastMassagesQuery, Bases.Response<List<MessageDTO>>>,
                 IRequestHandler<GetTotalDetailsForWorker, Bases.Response<TotalDTO>>

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

        public async Task<Response<List<ServiceDTO>>> Handle(GetLastServicesRequestByUserIdQuery request, CancellationToken cancellationToken)
        {
            //get 
            var user = await _WorkerService.GetUserByUserId(request.userId);
            if (user == null) return NotFound<List<ServiceDTO>>("المستخدم غير موجود");

            var WorkerId = await _WorkerService.GetWorkerIdByUserId(request.userId);

            var servicesList = await _WorkerService.GetLastServicesForWorkerAsync(WorkerId, request.SelectedNumber);

            //check
            if (servicesList == null || !servicesList.Any())
                return Success(new List<ServiceDTO>());

            //mapper
            var MappedList = _mapper.Map<List<ServiceDTO>>(servicesList);

            //return mapped
            return Success(MappedList);

        }

        public async Task<Response<List<LastReviewDTO>>> Handle(GetLastReviewsQuery request, CancellationToken cancellationToken)
        {
            //get..check
            var user = await _WorkerService.GetUserByUserId(request.userId);
            if (user == null) return NotFound<List<LastReviewDTO>>("المستخدم غير موجود");

            var WorkerId = await _WorkerService.GetWorkerIdByUserId(request.userId);

            //get list -- check
            var ReviewsList = await _WorkerService.GetLastReviewsForWorkerAsync(WorkerId, request.SelectedNumber);
            if (ReviewsList == null || !ReviewsList.Any())
                return Success(new List<LastReviewDTO>());

            //mapped
            var MappedList = _mapper.Map<List<LastReviewDTO>>(ReviewsList);

            //return maspped
            return Success(MappedList);
        }

        public async Task<Response<List<MessageDTO>>> Handle(GetLastMassagesQuery request, CancellationToken cancellationToken)
        {
            //get .. check
            var user = await _WorkerService.GetUserByUserId(request.UserId);
            if (user == null) return NotFound<List<MessageDTO>>("المستخدم غير موجود");

            //get..check
            var MassagesList = await _WorkerService.GetLastMessagessForWorkerAsync(request.UserId, request.SelectedNumber);
            if (MassagesList == null || !MassagesList.Any())
                return Success(new List<MessageDTO>());

            //mapp
            var MappedList = _mapper.Map<List<MessageDTO>>(MassagesList);

            //return mapped
            return Success(MappedList);
        }

        public async Task<Response<TotalDTO>> Handle(GetTotalDetailsForWorker request, CancellationToken cancellationToken)
        {
            //get .. check
            var user = await _WorkerService.GetUserByUserId(request.userId);
            if (user == null) return NotFound<TotalDTO>("المستخدم غير موجود");

            var WorkerId = await _WorkerService.GetWorkerIdByUserId(request.userId);

            var _totalReviews = await _WorkerService.GetLastReviewsForWorkerAsync(WorkerId, null);
            var numOfReviews = _totalReviews.Count;

            var _totalServices = await _WorkerService.GetLastServicesForWorkerAsync(WorkerId, null);
            var numOfServices = _totalServices.Count;

            var numOfPortfolios = await _WorkerService.GetTotalNumberOfPortfoliosForWorkerAsync(WorkerId);

            var numOfReports = await _WorkerService.GetTotalNumberOfReportsForWorkerAsync(request.userId);

            var total = new TotalDTO()
            {
                TotalNumberOfPortfolioes = numOfPortfolios,
                TotalNumberOfReviews = numOfReviews,
                TotalNumberOfServicesRequests = numOfServices,
                TotalNumberOfReportes = numOfReports,
            };

            return Success(total);
        }

        #endregion

    }
}
