using FixIt.Core.Bases;
using FixIt.Domain.Enum;
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

        [Required]
        public ReportStatus Status { get; set; } = ReportStatus.Pending;

        [Required(ErrorMessage = "يجب ترك ملاحظات عن سبب قفل البلاغ")]
        public string AdminNotes { get; set; }

    }
}
