using FixIt.Core.Bases;
using MediatR;

namespace FixIt.Core.Features.Workers.Commands.Models
{
    public record EditeWorkerCommand : IRequest<Response<string>>
    {

        public Guid UserId { get; set; } = Guid.NewGuid();
        public string FullName { get; set; }
        //public string Email { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
        public string? ImgUrl { get; set; }
        //public string Role { get; set; }
        public bool IsActive { get; set; } = false;
        //public DateTime? LastLogin { get; set; }
        //public DateTime CreatedAt { get; set; } = DateTime.Now;
        //public DateTime? UpdatedAt { get; set; }

        public string? JobTitle { get; set; }
        public string? Description { get; set; }
        public bool? AvailabilityStatus { get; set; } = false;
        public double? RatingAverage { get; set; }
        public string? Area { get; set; }
        public int? CategoryId { get; set; }

    }
}
