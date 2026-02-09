using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Domain.Entities
{
    public class Favorite
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [ForeignKey("Client")]
        public int ClientId { get; set; }
        public virtual User Client { get; set; }

        [ForeignKey("Worker")]
        public int WorkerId { get; set; }
        public virtual WorkerProfile Worker { get; set; }
    }
}
