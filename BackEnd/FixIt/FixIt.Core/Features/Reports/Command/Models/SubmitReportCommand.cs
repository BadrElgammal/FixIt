using FixIt.Core.Bases;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FixIt.Core.Features.Reports.Command.Models
{
    public class SubmitReportCommand : IRequest<Response<string>>
    {


        [Required(ErrorMessage = "عنوان البلاغ مطلوب")]
        [Length(5, 200)]
        public string Title { get; set; }

        [Required(ErrorMessage = "وصف المشكلة مطلوب")]
        [Length(5, 2000)]
        public string Description { get; set; }

        /// <summary>
        /// فئة البلاغ:
        /// - DelayInSchedule (تأخير عن الموعد)
        /// - UnprofessionalBehavior (سلوك غير لائق)
        /// - PriceManipulation (احتيال ومبالغ إضافية)
        /// - PoorQualityOrIncomplete (جودة ضعيفة)
        /// - PaymentIssue (مشاكل الدفع)
        /// - IdentityFraud (هوية مزيفة)
        /// - PropertyDamage (أضرار مادية)
        /// - Other (أخرى)
        /// </summary>
        [Required]
        public string ReportType { get; set; } = "Other";// ReportCategory.Other;


        // ["ReporterUser"]
        [JsonIgnore]
        public Guid ReporterUserId { get; set; }

        // ["ReportedUser"]
        [JsonIgnore]
        public Guid ReportedUserId { get; set; }

        // ["ServiceRequest"]
        public Guid? RequestId { get; set; }

    }
}
