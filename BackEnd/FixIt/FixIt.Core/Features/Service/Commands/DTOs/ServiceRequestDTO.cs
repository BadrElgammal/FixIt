using FixIt.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Core.Features.Service.Commands.DTOs
{
    public class ServiceRequestDTO
    {
        public Guid RequestId { get; set; } = Guid.NewGuid();
        public string ServiceTitle { get; set; }
        public string ServiceDescription { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal DepositAmount { get; set; }
        public string State { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime? CompleteDate { get; set; }
        public Guid ClientId { get; set; }
        public User Client { get; set; }
        public Guid WorkerId { get; set; }
        public WorkerProfile Worker { get; set; }

        public Review? Review { get; set; }
    }
}
