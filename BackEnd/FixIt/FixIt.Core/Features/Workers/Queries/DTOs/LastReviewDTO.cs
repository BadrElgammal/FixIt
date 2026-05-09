namespace FixIt.Core.Features.Workers.Queries.DTOs
{
    public class LastReviewDTO
    {
        public decimal Rate { get; set; }
        public string Comment { get; set; }

        //name & img
        public string ReviewerName { get; set; }
        public string? ReviewerImgUrl { get; set; }

    }
}
