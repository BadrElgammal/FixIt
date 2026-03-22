namespace FixIt.Core.Features.Portfolios.Queries.DTOs
{
    public class PortfolioDTO
    {
        public int PortfolioId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImgUrl { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // "WorkerProfile"
        public string WorkerFullName { get; set; }

    }
}
