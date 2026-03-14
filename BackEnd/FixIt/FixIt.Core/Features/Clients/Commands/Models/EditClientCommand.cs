using FixIt.Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FixIt.Core.Features.Clients.Commands.Models
{
    public class EditClientCommand : IRequest<Response<String>>
    {
        [JsonIgnore]
        public Guid UserId { get; set; } = Guid.NewGuid();

        public string FullName { get; set; }

        //public string Email { get; set; }

        public string Phone { get; set; }
        public string City { get; set; }

        //public string PasswordHash { get; set; }
        //public string? ImgUrl { get; set; }
        //public string Role { get; set; }
        //public bool IsActive { get; set; } = false;
        //public DateTime? LastLogin { get; set; }
        //public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
    }
}
