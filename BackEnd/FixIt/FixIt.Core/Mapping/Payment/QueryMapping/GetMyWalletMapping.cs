using FixIt.Core.Features.Payment.Queries.DTOs;
using FixIt.Core.Features.Payment.Queries.Models;
using FixIt.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Core.Mapping.Payment
{
    public partial class PaymentMapping
    {
        public void GetMyWalletMapping()
        {
            CreateMap<Wallet, WalletQueryDTO>();
        }
    }
}
