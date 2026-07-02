namespace FixIt.Domain.Enum
{
    public enum ReportCategory
    {
        // [Display(Name = "تأخير عن الموعد")]
        DelayInSchedule,

        //[Display(Name = "سلوك غير لائق أو سوء معاملة")]
        UnprofessionalBehavior,

        //[Display(Name = "مطالبة بمبالغ إضافية (نصب/احتيال)")]
        PriceManipulation,

        //[Display(Name = "جودة العمل ضعيفة أو غير مكتمل")]
        PoorQualityOrIncomplete,

        //[Display(Name = "رفض الدفع بعد الإنجاز")]
        PaymentIssue,

        //[Display(Name = "بيانات غير صحيحة (هوية مزيفة)")]
        IdentityFraud,

        //[Display(Name = "أضرار مادية في مكان العمل")]
        PropertyDamage,

        //[Display(Name = "أخرى")]
        Other
    }

}

