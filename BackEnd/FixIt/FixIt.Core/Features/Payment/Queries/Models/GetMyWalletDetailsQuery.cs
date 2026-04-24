using FixIt.Core.Bases;
using FixIt.Core.Features.Payment.Queries.DTOs;
using FixIt.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Core.Features.Payment.Queries.Models
{
    public class GetMyWalletDetailsQuery : IRequest<Response<WalletQueryDTO>>
    {
        public Guid UserId { get; set; }
        public GetMyWalletDetailsQuery(Guid userId)
        {
            UserId = userId;
        }
    }
}
