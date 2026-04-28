namespace FixIt.Core.Features.Workers.Queries.DTOs
{
    public class WorkerProfileDTO
    {

        public string? JobTitle { get; set; }
        public string? Description { get; set; }
        public bool AvailabilityStatus { get; set; } = false;
        public bool IsActive { get; set; } = false;
        public double? RatingAverage { get; set; }
        public string? Area { get; set; }

        public int? ReviewsCounter { get; set; }

        // "Category"
        public string CategoryName { get; set; }

        //"User"
        public string FullName { get; set; }
        public Guid UserId { get; set; } //



        //Img 
        public string? ImgUrl { get; set; }



    }
}
