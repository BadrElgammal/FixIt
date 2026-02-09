using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Domain.Entities
{
    public class ServiceRequest
    {
        [Key]
        public int RequestId { get; set; }
        public string ServiceTitle { get; set; }
        public string ServiceDescription { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal DepositAmount { get; set; }
        public string State { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime? CompleteDate { get; set; }

        [ForeignKey("Client")]
        public int ClientId { get; set; }
        public User Client { get; set; }
        [ForeignKey("Worker")]
        public int WorkerId { get; set; }
        public  WorkerProfile Worker { get; set; }

        public Review? Review { get; set; }
        
        public ICollection<Transaction>? Transactions { get; set; } = new List<Transaction>();
    }
}
