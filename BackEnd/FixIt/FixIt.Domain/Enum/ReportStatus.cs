using System.ComponentModel.DataAnnotations;

namespace FixIt.Domain.Enum
{
    public enum ReportStatus
    {

        [Display(Name = "قيد الانتظار")]
        Pending = 1,

        [Display(Name = "تحت المراجعة")]
        UnderInvestigation = 2,

        [Display(Name = " تم الحل")]
        Resolved = 3,

        [Display(Name = "مرفوض (بلاغ كيدي/غير صحيح)")]
        Dismissed = 4,

        [Display(Name = "تم التصعيد")]
        Escalated = 5       // محتاج تدخل قانوني

    }
}
