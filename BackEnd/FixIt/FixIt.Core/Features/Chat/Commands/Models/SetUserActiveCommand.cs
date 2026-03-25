using FixIt.Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Core.Features.Chat.Commands.Models
{
    public class SetUserActiveCommand :IRequest<Response<bool>>
    {
        public Guid UserId { get; set; }
        public bool IsActive { get; set; }

        public SetUserActiveCommand(Guid userId, bool isActive)
        {
            UserId = userId;
            IsActive = isActive;
        }
    }
}
