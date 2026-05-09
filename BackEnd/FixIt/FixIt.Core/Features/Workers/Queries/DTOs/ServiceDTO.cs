using FixIt.Domain.Enum;

namespace FixIt.Core.Features.Workers.Queries.DTOs
{
    public class ServiceDTO
    {

        //Service Request

        public string ServiceTitle { get; set; }
        public decimal TotalPrice { get; set; } = decimal.Zero;
        public ServiceRequestState State { get; set; }

        //Client Name & Img
        public string ClientName { get; set; }
        public string? ClientImgUrl { get; set; }

        //Worker Name & Img
        public string WorkerName { get; set; }
        public string? WorkerImgUrl { get; set; }



    }
}
