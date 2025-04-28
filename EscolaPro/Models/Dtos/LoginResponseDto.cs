using EscolaPro.Enums;
using System.Data;

namespace EscolaPro.Models.Dtos
{
    public class LoginResponseDto
    {
        public required string? Name { get; set; }
        public required long UserId { get; set; }
        public required string Token { get; set; }
        public required string RefreshToken { get; set; }
        public DateTime Expiration { get; set; }
        public RolesEnum? Role { get; set; }
    }
}
