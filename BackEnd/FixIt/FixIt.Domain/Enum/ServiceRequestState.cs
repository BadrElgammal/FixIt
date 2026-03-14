using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Domain.Enum
{
    public enum ServiceRequestState
    {
        priceprocess,
        rejected,
        pending,
        inprocess,
        canceled,
        submitted,
        disputed,
        reviewed,
        completed
    }
}
