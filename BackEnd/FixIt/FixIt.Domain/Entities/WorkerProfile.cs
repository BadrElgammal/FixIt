using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Domain.Entities
{
    public class WorkerProfile
    {
        [Key]
        public int WorkerId { get; set; }
        public string JobTitle { get; set; }
        public string Description { get; set; }
        public decimal ServiceBalance { get; set; }
        public bool AvailabilityStatus { get; set; }
        public double RatingAverage { get; set; }
        public string Area { get; set; }


        [ForeignKey("Category")]
        public int? CategoryId { get; set; } 
        public Category? Category { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<Portfolio>? Portfolios { get; set; } = new List<Portfolio>();
        public ICollection<ServiceRequest>? ReceivedRequests { get; set; } = new List<ServiceRequest>();
        public ICollection<Review>? Reviews { get; set; } = new List<Review>();
        public ICollection<Favorite>? Favorites { get; set; } = new List<Favorite>();
    }
}
