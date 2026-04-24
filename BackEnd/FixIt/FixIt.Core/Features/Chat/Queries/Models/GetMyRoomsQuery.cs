using FixIt.Core.Bases;
using FixIt.Core.Features.Chat.Queries.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Core.Features.Chat.Queries.Models
{
    public class GetMyRoomsQuery : IRequest<Response<List<MyRoomQueryDTO>>>
    {
        public Guid CurrentUserId { get; set; }
        public GetMyRoomsQuery(Guid currentUserId)
        {
            CurrentUserId = currentUserId;
        }
    }
}
