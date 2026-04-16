using AutoMapper;
using FixIt.Core.Bases;
using FixIt.Core.Features.Reviews.Query.DTOs;
using FixIt.Core.Features.Reviews.Query.Models;
using FixIt.Core.Wrapper;
using FixIt.Domain.Entities;
using FixIt.Service.Abstracts;
using MediatR;
using System.Linq.Expressions;

namespace FixIt.Core.Features.Reviews.Query.Handlers
{
    public class ReviewQueryHandler : ResponseHandler,
                IRequestHandler<GetReviewsListQuery, PaginatedResult<ReviewDTO>>,
                IRequestHandler<GetMyAllReviewsListQuery, PaginatedResult<ReviewDTO>>,
                IRequestHandler<GetReviewsListByWorkerIdQuery, PaginatedResult<ReviewForWorkerDTO>>
    {
        private readonly IReviewsService _reviewsService;
        private readonly IMapper _mapper;
        public ReviewQueryHandler(IReviewsService reviewsService, IMapper mapper)
        {
            _reviewsService = reviewsService;
            _mapper = mapper;
        }


        //for Admin
        public async Task<PaginatedResult<ReviewDTO>> Handle(GetReviewsListQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Review, ReviewDTO>> expression = e => new ReviewDTO(e.ReviewId, e.Rate, e.Comment, e.CreatedAt, e.Reviewer.FullName, e.Reviewer.ImgUrl, e.Reviewer.Role);
            var reviews = _reviewsService.GetAllReviewsPaginated();
            var ReviewsPaginatedList = await reviews.Select(expression).ToPaginatedListAsync(request.pageNum, request.pageSize);
            return ReviewsPaginatedList;

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
        public async Task<PaginatedResult<ReviewForWorkerDTO>> Handle(GetReviewsListByWorkerIdQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Review, ReviewForWorkerDTO>> expression = e => new ReviewForWorkerDTO(e.ReviewId, e.Rate, e.Comment, e.CreatedAt, e.ReviewerId, e.Reviewer.FullName, e.Reviewer.ImgUrl, e.ReviewedWorkerId, e.ReviewedWorker.User.FullName, e.ReviewedWorker.User.ImgUrl, e.RequestId);
            var reviews = _reviewsService.GetAllReviewsByWorkerIdpaginated(request.workerId);
            var paginatedList = await reviews.Select(expression).ToPaginatedListAsync(request.pageNum, request.pageSize);
            return paginatedList;
        }

        //for me 
        public async Task<PaginatedResult<ReviewDTO>> Handle(GetMyAllReviewsListQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Review, ReviewDTO>> expression = e => new ReviewDTO(e.ReviewId, e.Rate, e.Comment, e.CreatedAt, e.Reviewer.FullName, e.Reviewer.ImgUrl, e.Reviewer.Role);
            var workerId = await _reviewsService.GetWorkerIdByUserId(request.userId);
            var reviews = _reviewsService.GetAllReviewsByWorkerIdpaginated(workerId);
            var ReviewsPaginatedList = await reviews.Select(expression).ToPaginatedListAsync(request.pageNum, request.pageSize);
            return ReviewsPaginatedList;
        }


    }
}
