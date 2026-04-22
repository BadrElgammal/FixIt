using System.ComponentModel.DataAnnotations;

namespace FixIt.Domain.Enum
{
    public enum ReportCategory
    {
        [Display(Name = "تأخير عن الموعد")]
        DelayInSchedule = 1,

        [Display(Name = "سلوك غير لائق أو سوء معاملة")]
        UnprofessionalBehavior = 2,

        [Display(Name = "مطالبة بمبالغ إضافية (نصب/احتيال)")]
        PriceManipulation = 3,

        [Display(Name = "جودة العمل ضعيفة أو غير مكتمل")]
        PoorQualityOrIncomplete = 4,

        [Display(Name = "رفض الدفع بعد الإنجاز")]
        PaymentIssue = 5,

        [Display(Name = "بيانات غير صحيحة (هوية مزيفة)")]
        IdentityFraud = 6,

        [Display(Name = "أضرار مادية في مكان العمل")]
        PropertyDamage = 7,

        [Display(Name = "أخرى")]
        Other = 99
    }

}

