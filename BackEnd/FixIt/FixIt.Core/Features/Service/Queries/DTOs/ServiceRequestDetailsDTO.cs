namespace FixIt.Core.Features.Service.Queries.DTOs
{
    public class ServiceRequestDetailsDTO
    {
        public Guid ServiceId { get; set; }
        public string ServiceTitle { get; set; }
        public string ServiceDescription { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal DepositAmount { get; set; }
        public string State { get; set; }
        public string? serviceAddress { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime? CompleteDate { get; set; }
        public string ClientName { get; set; }
        public string WorkerName { get; set; }
        public Guid ClientId { get; set; }
        //public User Client { get; set; }
        public Guid WorkerId { get; set; }
        //public WorkerProfile Worker { get; set; }

        public int ReviewId { get; set; }
        public decimal Rate { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }

        //Imgs
        public string? RequestedImgUrl { get; set; }
        public string? SubmitedImgUrl { get; set; }


    }
}
