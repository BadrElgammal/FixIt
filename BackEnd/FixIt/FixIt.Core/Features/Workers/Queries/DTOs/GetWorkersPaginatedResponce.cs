using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Core.Features.Workers.Queries.DTOs
{
    public class GetWorkersPaginatedResponce
    {
        public Guid WorkerId { get; set; } = Guid.NewGuid();
        public string? JobTitle { get; set; }
        public string? Description { get; set; }
        public bool? AvailabilityStatus { get; set; } = false;
        public double? RatingAverage { get; set; }
        public string? Area { get; set; }
        public string? CategoryName { get; set; }

        public string FullName { get; set; }
        public string? ImgUrl { get; set; }
        public string Role { get; set; }
        public string City { get; set; }

        public GetWorkersPaginatedResponce(Guid workerId, string? jobTitle, string? description, bool? availabilityStatus, double? ratingAverage, string? area, string? categoryName, string fullName, string? imgUrl, string role, string city)
        {
            WorkerId = workerId;
            JobTitle = jobTitle;
            Description = description;
            AvailabilityStatus = availabilityStatus;
            RatingAverage = ratingAverage;
            Area = area;
            CategoryName = categoryName;
            FullName = fullName;
            ImgUrl = imgUrl;
            Role = role;
            City = city;
        }
    }
}
