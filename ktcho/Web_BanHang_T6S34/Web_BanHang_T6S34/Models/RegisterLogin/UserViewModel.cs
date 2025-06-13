using System.ComponentModel.DataAnnotations;

namespace Web_BanHang_T6S34.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Email là bắt buộc.")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
        public string Email { get; set; }

        public List<string> Roles { get; set; }

        public string AvatarUrl { get; set; }

        public bool IsDisabled { get; set; }
    }
}