using AutoMapper;
using FixIt.Core.Bases;
using FixIt.Core.Features.Reports.Command.Models;
using FixIt.Domain.Entities;
using FixIt.Domain.Enum;
using FixIt.Service.Abstracts;
using MediatR;

namespace FixIt.Core.Features.Reports.Command.Handlers
{
    public class ReportCommandHandler : ResponseHandler,
                             IRequestHandler<SubmitReportCommand, Response<string>>,
                             IRequestHandler<ResolveReportByAdminCommand, Response<string>>
    {

        private readonly IReportService _reportService;
        private readonly IService<User> _userService;
        private readonly IServiceRequestService _serviceRequest;
        private readonly IMapper _mapper;

        public ReportCommandHandler(IReportService reportService, IMapper mapper,
            IService<User> userService, IServiceRequestService serviceRequest)
        {
            _reportService = reportService;
            _mapper = mapper;
            _userService = userService;
            _serviceRequest = serviceRequest;
        }


        public async Task<Response<string>> Handle(SubmitReportCommand request, CancellationToken cancellationToken)
        {
            //worker => user =>exist or no
            var ReportedUser = await _userService.GetByIdAsync(request.ReportedUserId);
            if (ReportedUser == null) return BadRequest<string>("المستخدم المقدم فيه البلاغ ليس موجود ");

            //CurrentUser
            //var CurrentUserId = _userService

            if (request.RequestId.HasValue)
            {
                //if exist => check
                var serviceRequest = await _serviceRequest.GetServiceRequestWithAllData(request.RequestId);
                if (serviceRequest == null) return BadRequest<string>("الخدمة المشار اليها ليست موجودة ");

                //get UserId by WorkerId
                var UserIdFromWorkerId = await _serviceRequest.GetUserIdByWorkerId(serviceRequest.WorkerId);
                if (UserIdFromWorkerId != request.ReporterUserId && serviceRequest.ClientId != request.ReporterUserId)
                    return BadRequest<string>("لا يمكنك عمل بلاغ عن طلب ولست طرفا فيه ");

            }

            if (request.ReporterUserId == request.ReportedUserId)
                return BadRequest<string>("لا يمكنك عمل ابلاغ عن نفسك ");


            //mapp
            var MappedReport = _mapper.Map<Report>(request);

            //Add to DB
            var result = await _reportService.AddReportAsync(MappedReport);


            //check
            if (result == "success")
                return Success("تم تقديم ابلاغ بنجاح سيقوم الادمن بمراجعته");

            return BadRequest<string>("حدث خطا ما");
        }

        public async Task<Response<string>> Handle(ResolveReportByAdminCommand request, CancellationToken cancellationToken)
        {
            //get report
            var report = await _reportService.GetByIdAsync(request.ReportId);
            if (report == null) return BadRequest<string>("البلاغ غير موجود");

            //check => تم الحل || مرفوض || كيدي
            if (report.Status == ReportStatus.Resolved || report.Status == ReportStatus.Dismissed
               || report.Status == ReportStatus.Escalated)
                return BadRequest<string>("هذا البلاغ تم إغلاقه أو التعامل معه مسبقاً ولا يمكن تعديله.");


            //solve 
            if (request.Status == ReportStatus.Pending || request.Status == ReportStatus.UnderInvestigation)
            {
                return BadRequest<string>("يجب اختيار حالة نهائية مثل (تم الحل، مرفوض، أو تم التصعيد).");
            }

            //full data
            report.Status = request.Status;
            report.AdminNotes = request.AdminNotes;
            report.ResolvedAt = DateTime.Now;

            //update
            var result = await _reportService.UpdateReportAsync(report);

            //success?
            if (result == "success") return Success("تم التعامل مع البلاغ وتحديث حالته بنجاح");
            return BadRequest<string>("حدث خطأ أثناء تحديث البلاغ");

        }
    }
}
