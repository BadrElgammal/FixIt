using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Domain.Enum
{
    public enum TransactionType
    {
        PayDeposit,      //من العميل للسيستم 
        ReleaseToWorker, // من السيستم للوركر
        RefundToClient   // لو الخدمه اتلغت والفلوس رجعت للعميل
    }
}
