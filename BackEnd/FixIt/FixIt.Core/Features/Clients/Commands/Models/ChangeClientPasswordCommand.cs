using FixIt.Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Core.Features.Clients.Commands.Models
{
    public class ChangeClientPasswordCommand : IRequest<Response<String>>
    {
        public Guid UserId { get; set; } = Guid.NewGuid();
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfarmPassword { get; set; }
    }
}
