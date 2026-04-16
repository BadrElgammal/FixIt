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
        public string? ReviewerImgUrl { get; set; }
        public string ReviewerRole { get; set; }



        ////"ReviewedWorker"
        //public string ReviewerWorkerName { get; set; }
        ////"Request"
        //[JsonIgnore]
        //public Guid RequestId { get; set; }

        public ReviewDTO(int reviewId , decimal rate , string comment , DateTime createdAt , string reviewerName , string? reviewerImgUrl , string reviewerRole)
        {
            ReviewId = reviewId;
            Rate = rate;
            Comment = comment;
            CreatedAt = createdAt;
            ReviewerName = reviewerName;
            ReviewerImgUrl = reviewerImgUrl;
            ReviewerRole = reviewerRole;
        }

    }
}
