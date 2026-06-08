using AutoMapper;
using FixIt.Core.Bases;
using FixIt.Core.Features.Reviews.Command.Models;
using FixIt.Domain.Entities;
using FixIt.Infrastructure.Context;
using FixIt.Service.Abstracts;
using FixIt.Service.Services;
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
        private readonly IWorkerService _workerService;

        public ReviewCommandHandler(IReviewsService reviewsService, IMapper mapper, FIXITDbContext db, IServiceRequestService serviceRequestService , IWorkerService workerService)
        {

            _reviewsService = reviewsService;
            _mapper = mapper;
            _db = db;
            _serviceRequestService = serviceRequestService;
            _workerService = workerService;
        }

        public async Task<Response<string>> Handle(AddReviewCommand request, CancellationToken cancellationToken)
        {
            var serviceRequest = _serviceRequestService.Find(s => s.RequestId == request.RequestId).FirstOrDefault();

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

                // إذا تم إضافة التقييم بنجاح، نقوم بتحديث متوسط تقييم الفني
                if (result == "success")
                {
                    // 1. حساب المتوسط الجديد للفني
                    // الأفضل استخدام دالة تحسب المتوسط في قاعدة البيانات مباشرة لتوفير الأداء
                    var workerReviews = await _reviewsService.GetAllReviewsByWorkerIdAsync(serviceRequest.WorkerId);

                    // حساب المتوسط باستخدام LINQ (تتأكد إن فيه تقييمات عشان تتجنب القسمة على صفر)
                    double newAverageRate = workerReviews.Any()
                                            ? (double)workerReviews.Average(r => r.Rate)
                                            : (double) request.Rate;

                    // 2. تحديث جدول الفني
                    var worker = await _workerService.GetWorkerByWorkerId(serviceRequest.WorkerId); // استخدم اسم الدالة الصحيح عندك
                    if (worker != null)
                    {
                        // تقريب الرقم العشري لعلامة واحدة (مثلاً 4.5 بدل 4.5333)
                        worker.RatingAverage = Math.Round(newAverageRate, 1);
                        await _workerService.EditeWorkerAsync(worker); // استخدم اسم دالة التعديل الصحيحة
                    }

                    return Success("تم اضافة التقييم بنجاح");
                }
                else
                {
                    return BadRequest<string>("حدث خطأ أثناء إضافة التقييم");
                }
            }

            return BadRequest<string>("لا يوجد طلب خدمة لهذا العميل");
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
