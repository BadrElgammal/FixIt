using FixIt.Core.Bases;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FixIt.Core.Features.Admin.Command.Models
{
    public class EditeAdminCommand : IRequest<Response<string>>
    {

        [JsonIgnore]
        public Guid UserId { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "الاسم مطلوب")]
        [StringLength(100)]
        [MinLength(3)]
        public string FullName { get; set; }

        [RegularExpression(@"^(010|011|012|015)[0-9]{8}$")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "اسم المدينه مطلوب")]
        public string City { get; set; }
        public DateTime? UpdatedAt { get; set; }


    }
}
