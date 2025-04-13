namespace EscolaPro.Models.Dtos
{
    public class UserGeneralDto
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public Roles Role { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }
        public DateTime LastAccess { get; set; }
    }
}
