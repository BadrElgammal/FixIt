using FixIt.Core.Bases;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FixIt.Core.Features.Reports.Command.Models
{
    public class ResolveReportByAdminCommand : IRequest<Response<string>>
    {
        //Reportdto

        [JsonIgnore]
        public int ReportId { get; set; }

        /// <summary>
        /// حالة البلاغ
        /// --Pending  "قيد الانتظار"
        /// --UnderInvestigation  "تحت المراجعة"
        /// --Resolved  " تم الحل"
        /// --Dismissed   "مرفوض (بلاغ كيدي/غير صحيح)"
        /// --Escalated "تم التصعيد/تدخل قانوني"
        /// </summary>
        [Required]
        public string Status { get; set; } = "Pending";// = ReportStatus.Pending;

        [Required(ErrorMessage = "يجب ترك ملاحظات عن سبب قفل البلاغ")]
        public string AdminNotes { get; set; }

    }
}
