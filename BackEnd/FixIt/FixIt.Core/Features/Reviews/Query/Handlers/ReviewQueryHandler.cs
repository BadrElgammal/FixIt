using AutoMapper;
using FixIt.Core.Bases;
using FixIt.Core.Features.Reviews.Query.DTOs;
using FixIt.Core.Features.Reviews.Query.Models;
using FixIt.Service.Abstracts;
using MediatR;

namespace FixIt.Core.Features.Reviews.Query.Handlers
{
    public class ReviewQueryHandler : ResponseHandler,
                IRequestHandler<GetReviewsListQuery, Response<List<ReviewDTO>>>,
                IRequestHandler<GetMyAllReviewsListQuery, Response<List<ReviewDTO>>>,
                IRequestHandler<GetReviewsListByWorkerIdQuery, Response<ReviewForWorkerDTO>>
    {
        private readonly IReviewsService _reviewsService;
        private readonly IMapper _mapper;
        public ReviewQueryHandler(IReviewsService reviewsService, IMapper mapper)
        {
            _reviewsService = reviewsService;
            _mapper = mapper;
        }


        //for Admin
        public async Task<Response<List<ReviewDTO>>> Handle(GetReviewsListQuery request, CancellationToken cancellationToken)
        {
            var Reviews = await _reviewsService.GetAllReviewsAsync();
            if (Reviews == null || !Reviews.Any()) return NotFound<List<ReviewDTO>>("لا يوجد ");

            var ReviewsMapper = _mapper.Map<List<ReviewDTO>>(Reviews);
            return Success(ReviewsMapper);

        }

        //for another worker
        //public async Task<Response<List<ReviewDTO>>> Handle(GetReviewsListByWorkerIdQuery request, CancellationToken cancellationToken)
        //{

        //    var Reviews = await _reviewsService.GetAllReviewsByWorkerIdAsync(request.workerId);
        //    if (Reviews == null || !Reviews.Any()) return NotFound<List<ReviewDTO>>("لا يوجد ");

        //    var ReviewsMapper = _mapper.Map<List<ReviewDTO>>(Reviews);
        //    return Success(ReviewsMapper);


        //}

        ///for another worker
        public async Task<Response<ReviewForWorkerDTO>> Handle(GetReviewsListByWorkerIdQuery request, CancellationToken cancellationToken)
        {
            var worker = await _reviewsService.GetWorkerByWorkerId(request.workerId);

            if (worker == null) return NotFound<ReviewForWorkerDTO>("هذا العامل غير موجود");
            if (worker.Reviews == null || !worker.Reviews.Any()) return NotFound<ReviewForWorkerDTO>("لا يوجد ريفيوز ");


            var ReviewsMapper = _mapper.Map<ReviewForWorkerDTO>(worker);
            return Success(ReviewsMapper);


        }

        //for me 
        public async Task<Response<List<ReviewDTO>>> Handle(GetMyAllReviewsListQuery request, CancellationToken cancellationToken)
        {
            var workerId = await _reviewsService.GetWorkerIdByUserId(request.userId);
            var Reviews = await _reviewsService.GetAllReviewsByWorkerIdAsync(workerId);
            if (Reviews == null || !Reviews.Any()) return NotFound<List<ReviewDTO>>("لا يوجد ");

            var ReviewsMapper = _mapper.Map<List<ReviewDTO>>(Reviews);
            return Success(ReviewsMapper);

        }


    }
}
