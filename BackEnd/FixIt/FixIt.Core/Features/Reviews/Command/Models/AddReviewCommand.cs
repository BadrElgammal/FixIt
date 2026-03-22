using FixIt.Core.Bases;
using MediatR;
using System.Text.Json.Serialization;

namespace FixIt.Core.Features.Reviews.Command.Models
{
    public class AddReviewCommand : IRequest<Response<string>>
    {
        public decimal Rate { get; set; }
        public string Comment { get; set; }
        [JsonIgnore]
        public Guid RequestId { get; set; }

        [JsonIgnore]
        public Guid ReviewedWorkerId { get; set; }
        [JsonIgnore]
        public Guid ReviewerId { get; set; }
        [JsonIgnore]
        public DateTime CreatedAt { get; set; }

    }
}
