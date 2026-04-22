using FixIt.Domain.Enum;

namespace FixIt.Core.Features.Reports.Queries.DTOs
{
    public class ReportDTO
    {
        //Reort => 
        /*
         * 
         * 
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


        public Guid ReportedUserId { get; set; }


        [ForeignKey("ServiceRequest")]
        public Guid? RequestId { get; set; }
         
         */

        //Report 

        public int ReportId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public ReportStatus Status { get; set; }
        public ReportCategory ReportType { get; set; }
        public string? AdminNotes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ResolvedAt { get; set; }

        //["ReporterUser"] => name , ,,, 
        public Guid ReporterUserId { get; set; }
        public string ReporterUserName { get; set; }

        //["ReportedUser"] => name , ,,, 
        public Guid ReportedUserId { get; set; }
        public string ReportedUserName { get; set; }


        // ["ServiceRequest"] =>
        public Guid? RequestId { get; set; }


    }
}
