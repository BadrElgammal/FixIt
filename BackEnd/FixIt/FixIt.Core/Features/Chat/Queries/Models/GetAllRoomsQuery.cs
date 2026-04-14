using FixIt.Core.Features.Chat.Queries.DTOs;
using FixIt.Core.Wrapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Core.Features.Chat.Queries.Models
{
    public class GetAllRoomsQuery : IRequest<PaginatedResult<AllRoomsQueryDTO>>
    {
        public int pageNum { get; set; }
        public int pageSize { get; set; }
    }
}
