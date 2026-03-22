using AutoMapper;
using FixIt.Core.Bases;
using FixIt.Core.Features.Reviews.Query.DTOs;
using FixIt.Core.Features.Reviews.Query.Models;
using FixIt.Service.Abstracts;
using MediatR;

namespace FixIt.Core.Features.Reviews.Query.Handlers
{
    public class ReviewQueryHandler : ResponseHandler,
                IRequestHandler<GetReviewsListQuery, Response<List<ReviewDTO>>>
    {
        private readonly IReviewsService _reviewsService;
        private readonly IMapper _mapper;
        public ReviewQueryHandler(IReviewsService reviewsService, IMapper mapper)
        {
            _reviewsService = reviewsService;
            _mapper = mapper;
        }



        public async Task<Response<List<ReviewDTO>>> Handle(GetReviewsListQuery request, CancellationToken cancellationToken)
        {
            var Reviews = await _reviewsService.GetAllReviewsAsync();
            if (Reviews == null || !Reviews.Any()) return NotFound<List<ReviewDTO>>("لا يوجد ");

            var ReviewsMapper = _mapper.Map<List<ReviewDTO>>(Reviews);
            return Success(ReviewsMapper);

        }



    }
}
