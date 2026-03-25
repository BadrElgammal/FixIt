using FixIt.Domain.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FixIt.Domain.Entities
{
    public class ServiceRequest
    {
        [Key]
        public Guid RequestId { get; set; } = Guid.NewGuid();
        public string ServiceTitle { get; set; }
        public string ServiceDescription { get; set; }
        public decimal TotalPrice { get; set; } = decimal.Zero;
        public decimal DepositAmount { get; set; } = decimal.Zero;
        public ServiceRequestState State { get; set; }
        public DateTime RequestDate { get; set; } = DateTime.Now;
        public DateTime? CompleteDate { get; set; }

        public string? serviceAddress { get; set; }
        // Img
        public string? RequestedImgUrl { get; set; }
        public string? SubmitedImgUrl { get; set; }


        [ForeignKey("Client")]
        public Guid ClientId { get; set; }
        public User Client { get; set; }
        [ForeignKey("Worker")]
        public Guid WorkerId { get; set; }
        public WorkerProfile Worker { get; set; }

        public Review? Review { get; set; }

        public ICollection<Transaction>? Transactions { get; set; } = new List<Transaction>();
    }
}
