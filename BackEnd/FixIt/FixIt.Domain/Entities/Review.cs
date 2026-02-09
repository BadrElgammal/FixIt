using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public int ReviewerId { get; set; }
        public User Reviewer { get; set; }
        [ForeignKey("ReviewedWorker")]
        public int ReviewedWorkerId { get; set; }
        public WorkerProfile ReviewedWorker { get; set; }
        [ForeignKey("Request")]
        public int RequestId { get; set; }
        public ServiceRequest Request { get; set; }
    }
}
