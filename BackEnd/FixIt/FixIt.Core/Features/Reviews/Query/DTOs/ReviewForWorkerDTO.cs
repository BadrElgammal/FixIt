using FixIt.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace FixIt.Core.Features.Reviews.Query.DTOs
{
    public class ReviewForWorkerDTO
    {
        public int ReviewId { get; set; }
        public decimal Rate { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public Guid ReviewerId { get; set; }
        public string ReviewerName { get; set; }
        public string ReviewerImgUrl { get; set;}
        public Guid ReviewedWorkerId { get; set; }
        public string WorkerName { get; set; }
        public string? WorkerImgUrl { get; set; }
        public Guid RequestId { get; set; }

        public ReviewForWorkerDTO(int reviewId, decimal rate, string comment, DateTime createdAt, Guid reviewerId, string reviewerName, string reviewerImgUrl, Guid reviewedWorkerId, string workerName, string? workerImgUrl, Guid requestId)
        {
            ReviewId = reviewId;
            Rate = rate;
            Comment = comment;
            CreatedAt = createdAt;
            ReviewerId = reviewerId;
            ReviewerName = reviewerName;
            ReviewerImgUrl = reviewerImgUrl;
            ReviewedWorkerId = reviewedWorkerId;
            WorkerName = workerName;
            WorkerImgUrl = workerImgUrl;
            RequestId = requestId;
        }
    }
}
