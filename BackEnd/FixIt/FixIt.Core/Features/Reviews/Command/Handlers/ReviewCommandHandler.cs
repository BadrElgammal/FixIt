using AutoMapper;
using FixIt.Core.Bases;
using FixIt.Core.Features.Reviews.Command.Models;
using FixIt.Domain.Entities;
using FixIt.Infrastructure.Context;
using FixIt.Service.Abstracts;
using MediatR;

namespace FixIt.Core.Features.Reviews.Command.Handlers
{
    public class ReviewCommandHandler : ResponseHandler,
               IRequestHandler<AddReviewCommand, Response<string>>,
               IRequestHandler<DeleteReviewCommand, Response<string>>
    {
        private readonly IReviewsService _reviewsService;
        private readonly IMapper _mapper;
        private readonly FIXITDbContext _db;
        private readonly IServiceRequestService _serviceRequestService;

        public ReviewCommandHandler(IReviewsService reviewsService, IMapper mapper, FIXITDbContext db, IServiceRequestService serviceRequestService)
        {

            _reviewsService = reviewsService;
            _mapper = mapper;
            _db = db;
            _serviceRequestService = serviceRequestService;
        }

        public async Task<Response<string>> Handle(AddReviewCommand request, CancellationToken cancellationToken)
        {
            var serviceRequest = _serviceRequestService.Find(s => s.RequestId == request.RequestId)
                                     .FirstOrDefault();

            if (serviceRequest == null)
                return BadRequest<string>("لا يوجد طلب خدمة لإضافة تقييم له");

            if (serviceRequest.ClientId == request.ReviewerId
                && serviceRequest.State == Domain.Enum.ServiceRequestState.completed)
            {

                var Review = new Review()
                {

                    Rate = request.Rate,
                    Comment = request.Comment,
                    ReviewerId = request.ReviewerId,
                    ReviewedWorkerId = serviceRequest.WorkerId,
                    RequestId = serviceRequest.RequestId,
                    CreatedAt = DateTime.UtcNow

                };
                serviceRequest.State = Domain.Enum.ServiceRequestState.reviewed;

                var result = await _reviewsService.AddReviewsAsync(Review);
                await _serviceRequestService.EditServiceRequestAsync(serviceRequest);

                if (result == "success") return Success($"تم اضافة ");
                else return BadRequest<string>();


            }
            return BadRequest<string>("لا يوجد طلب خدمة لهذا العميل ");





        }

        public async Task<Response<string>> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
        {

            var review = await _reviewsService.GetReviewByIdAsync(request.ReviewId);
            if (review == null) return NotFound<string>("غير موجود");

            var result = await _reviewsService.DeleteReviewAsync(review);
            if (result == "success") return Success("تم الحذف ");
            else return BadRequest<string>();

        }
    }
}
