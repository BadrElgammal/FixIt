using FixIt.Domain.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FixIt.Domain.Entities
{
    public class Report
    {

        [Key]
        public int ReportId { get; set; }

        [Required(ErrorMessage = "عنوان البلاغ مطلوب")]
        [Length(5, 200)]
        public string Title { get; set; }

        [Required(ErrorMessage = "وصف المشكلة مطلوب")]
        [Length(5, 2000)]
        public string Description { get; set; }

        public ReportStatus Status { get; set; } = ReportStatus.Pending;
        [Required]
        public ReportCategory ReportType { get; set; } = ReportCategory.Other;

        public string? AdminNotes { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? ResolvedAt { get; set; }

        [ForeignKey("ReporterUser")]
        public Guid ReporterUserId { get; set; }
        public User ReporterUser { get; set; }


        [ForeignKey("ReportedUser")]
        public Guid ReportedUserId { get; set; }
        public User ReportedUser { get; set; }


        [ForeignKey("ServiceRequest")]
        public Guid? RequestId { get; set; }
        public ServiceRequest? ServiceRequest { get; set; }
    }
}