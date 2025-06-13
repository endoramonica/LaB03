using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Web_BanHang_T6S34.Models.RegisterLogin
{
    public class ProfileViewModel
    {
        [Required(ErrorMessage = "Email là bắt buộc.")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
        public string Email { get; set; }

        public string AvatarUrl { get; set; }

        public IFormFile Avatar { get; set; }
    }
}