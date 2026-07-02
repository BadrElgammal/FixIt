namespace FixIt.Domain.Enum
{
    public enum ReportStatus
    {

        // [Display(Name = "قيد الانتظار")]
        Pending,

        // [Display(Name = "تحت المراجعة")]
        UnderInvestigation,

        //[Display(Name = " تم الحل")]
        Resolved,

        // [Display(Name = "مرفوض (بلاغ كيدي/غير صحيح)")]
        Dismissed,

        // [Display(Name = "تم التصعيد")]
        Escalated

    }
}
