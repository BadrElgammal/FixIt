using FixIt.Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Core.Features.Chat.Commands.Models
{
    public class CreateOrGetRoomCommand : IRequest<Response<int>>
    {
        public Guid currentUserId;
        public Guid targetUserId;

        public CreateOrGetRoomCommand(Guid CurrentUserId, Guid TargetUserId)
        {
            currentUserId = CurrentUserId;
            targetUserId = TargetUserId;
        }
    }
}
