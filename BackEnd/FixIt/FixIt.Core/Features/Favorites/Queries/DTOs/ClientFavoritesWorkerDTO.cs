using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Core.Features.Favorites.Queries.DTOs
{
    public  class ClientFavoritesWorkerDTO
    {
        public Guid WorkerId { get; set; }

        public string FullName { get; set; }
        public string? ImgUrl { get; set; }
        public string Role { get; set; }
        public string City { get; set; }


        public string? Area { get; set; }
        public string? JobTitle { get; set; }
        public string? Description { get; set; }
        public bool AvailabilityStatus { get; set; } = false;
        public double? RatingAverage { get; set; }


        public string CategoryName { get; set; }
    }
}
