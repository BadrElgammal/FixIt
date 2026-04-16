namespace FixIt.Core.Features.Portfolios.Queries.DTOs
{
    public class PortfoliosForWorkerDTO
    {
        public string WorkerName { get; set; }
        public string? WorkerImgUrl { get; set; }
        public int PortfolioId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImgUrl { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;


        public PortfoliosForWorkerDTO(string workerName, string? workerImgUrl, int portfolioId, string title, string description, string imgUrl, DateTime createdAt)
        {
            WorkerName = workerName;
            WorkerImgUrl = workerImgUrl;
            PortfolioId = portfolioId;
            Title = title;
            Description = description;
            ImgUrl = imgUrl;
            CreatedAt = createdAt;
        }
    }
}
