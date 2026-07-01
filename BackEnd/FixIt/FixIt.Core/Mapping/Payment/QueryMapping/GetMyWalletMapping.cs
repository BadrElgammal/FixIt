using FixIt.Core.Features.Payment.Queries.DTOs;
using FixIt.Domain.Entities;

namespace FixIt.Core.Mapping.Payment
{
    public partial class PaymentMapping
    {
        public void GetMyWalletMapping()
        {
            CreateMap<Wallet, WalletQueryDTO>();
            CreateMap<FixIt.Domain.Entities.Payment, PaymentDTO>();
        }
    }
}
