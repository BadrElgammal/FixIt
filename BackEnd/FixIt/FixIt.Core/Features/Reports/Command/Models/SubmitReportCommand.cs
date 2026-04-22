using FixIt.Core.Bases;
using FixIt.Domain.Enum;
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

        [Required]
        public ReportCategory ReportType { get; set; } = ReportCategory.Other;


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
