namespace FixIt.Domain.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }

        public ICollection<WorkerProfile>? Workers { get; set; } = new List<WorkerProfile>();
    }
}
