using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FixIt.Domain.Entities
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }
        public decimal Rate { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }

        [ForeignKey("Reviewer")]
        public Guid ReviewerId { get; set; }
        public User Reviewer { get; set; }
        [ForeignKey("ReviewedWorker")]
        public Guid ReviewedWorkerId { get; set; }
        public WorkerProfile ReviewedWorker { get; set; }
        [ForeignKey("Request")]
        public Guid RequestId { get; set; }
        public ServiceRequest Request { get; set; }
    }
}
