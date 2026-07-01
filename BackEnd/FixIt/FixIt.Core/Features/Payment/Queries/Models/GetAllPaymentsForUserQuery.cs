using FixIt.Core.Bases;
using FixIt.Core.Features.Payment.Queries.DTOs;
using MediatR;

namespace FixIt.Core.Features.Payment.Queries.Models
{
    public class GetAllPaymentsForUserQuery : IRequest<Response<List<PaymentDTO>>>
    {
        public Guid UserId { get; set; }

        public GetAllPaymentsForUserQuery(Guid id)
        {
            UserId = id;
        }
    }
}
