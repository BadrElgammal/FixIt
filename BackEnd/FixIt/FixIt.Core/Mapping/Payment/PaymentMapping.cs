using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Core.Mapping.Payment
{
    public partial class PaymentMapping : Profile
    {
        public PaymentMapping()
        {
            GetMyWalletMapping();
        }
    }
}
