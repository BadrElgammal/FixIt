namespace FixIt.Core.Features.Admin.Query.DTOs
{
    public class AdminProfileDTO
    {
        public Guid UserId { get; set; } = Guid.NewGuid();
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }

        public string? ImgUrl { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; } = false;
        public DateTime? LastLogin { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }



    }
}
