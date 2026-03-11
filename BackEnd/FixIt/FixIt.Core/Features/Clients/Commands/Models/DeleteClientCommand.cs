using FixIt.Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Core.Features.Clients.Commands.Models
{
    public class DeleteClientCommand : IRequest<Response<String>>
    {
        public Guid Id { get; set; }
        public DeleteClientCommand(Guid Id)
        {
            this.Id = Id;
        }
    }
}
