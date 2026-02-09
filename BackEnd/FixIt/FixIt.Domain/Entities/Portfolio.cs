using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Domain.Entities
{
    public class Portfolio
    {
        public int PortfolioId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImgUrl { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [ForeignKey("WorkerProfile")]
        public int WorkerProfileId { get; set; }
        public WorkerProfile WorkerProfile { get; set; }
    }
}
