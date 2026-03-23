using FixIt.Core.Bases;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FixIt.Core.Features.Workers.Commands.Models
{
    public record EditeWorkerCommand : IRequest<Response<string>>
    {
        [JsonIgnore]
        public Guid UserId { get; set; }
        public EditeWorkerCommand(Guid UserId)
        {
            this.UserId = UserId;
        }

        //User

        [Required(ErrorMessage = "الاسم مطلوب")]
        [StringLength(100)]
        [MinLength(3)]
        public string FullName { get; set; }

        [RegularExpression(@"^(010|011|012|015)[0-9]{8}$")]
        public string Phone { get; set; }
        public string City { get; set; }
        public string? ImgUrl { get; set; }

        //WorkerProfile

        public string? JobTitle { get; set; }
        public string? Description { get; set; }
        public decimal? ServiceBalance { get; set; }
        public bool AvailabilityStatus { get; set; } = false;
        public string? Area { get; set; }

        //"Category"
        public string CategoryName { get; set; }



    }
}
