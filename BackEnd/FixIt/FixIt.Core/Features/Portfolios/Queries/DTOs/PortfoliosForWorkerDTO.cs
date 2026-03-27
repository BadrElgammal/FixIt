namespace FixIt.Core.Features.Portfolios.Queries.DTOs
{
    public class PortfoliosForWorkerDTO
    {
        public string WorkerName { get; set; }
        public string? WorkerImgUrl { get; set; }
        public List<PortfolioDTO>? PortfoliosList { get; set; }

    }
}
