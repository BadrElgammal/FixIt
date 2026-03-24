using FixIt.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Core.Features.Service.Queries.DTOs
{
    public class GetAllServiceRequistDTO
    {
        public Guid ServiceId { get; set; } 
        public string ServiceTitle { get; set; }
        public string ServiceDescription { get; set; }
        public decimal TotalPrice { get; set; }
        //public decimal DepositAmount { get; set; }
        public string State { get; set; }
        public DateTime RequestDate { get; set; }
        //public DateTime? CompleteDate { get; set; }
        public string ClientName { get; set; }
        public string WorkerName { get; set; }
        public Guid ClientId { get; set; }
        //public User Client { get; set; }
        public Guid WorkerId { get; set; }
        //public WorkerProfile Worker { get; set; }

        //public Review? Review { get; set; }
    }
}
