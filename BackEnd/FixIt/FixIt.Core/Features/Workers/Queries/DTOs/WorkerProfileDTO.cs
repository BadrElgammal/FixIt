namespace FixIt.Core.Features.Workers.Queries.DTOs
{
    public class WorkerProfileDTO
    {

        public string? JobTitle { get; set; }
        public string? Description { get; set; }
        public decimal? ServiceBalance { get; set; }
        public bool AvailabilityStatus { get; set; } = false;
        public double? RatingAverage { get; set; }
        public string? Area { get; set; }

        // "Category"
        public string CategoryName { get; set; }

        //"User"
        public string FullName { get; set; }


    }
}
