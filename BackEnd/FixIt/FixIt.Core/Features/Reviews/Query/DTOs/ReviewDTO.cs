namespace FixIt.Core.Features.Reviews.Query.DTOs
{
    public class ReviewDTO
    {

        public int ReviewId { get; set; }
        public decimal Rate { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }

        //"Reviewer"
        public string ReviewerName { get; set; }

        //"ReviewedWorker"
        public string ReviewerWorkerName { get; set; }
        //"Request"
        public Guid RequestId { get; set; }

    }
}
