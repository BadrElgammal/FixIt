using FixIt.Core.Bases;
using MediatR;
using System.Text.Json.Serialization;

namespace FixIt.Core.Features.Favorites.Commands.Models
{
    public class AddFavoriteCommand : IRequest<Response<string>>
    {
        [JsonIgnore]
        public Guid ClientId { get; set; }
        public Guid WorkerId { get; set; }

        public AddFavoriteCommand(Guid WorkerId)
        {
            this.WorkerId = WorkerId;
        }

    }
}
