namespace FixIt.Core.Features.Reviews.Query.DTOs
{
    public class ReviewForWorkerDTO
    {
        public string WorkerName { get; set; }
        public string? ImgUrl { get; set; }
        public List<ReviewDTO>? Reviews { get; set; }


    }
}
