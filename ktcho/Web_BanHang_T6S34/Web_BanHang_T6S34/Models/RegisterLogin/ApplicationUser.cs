using Microsoft.AspNetCore.Identity;

namespace Web_BanHang_T6S34.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? AvatarUrl { get; set; }
        public bool IsDisabled { get; set; }
        public string? SessionToken { get; set; }
    }
}