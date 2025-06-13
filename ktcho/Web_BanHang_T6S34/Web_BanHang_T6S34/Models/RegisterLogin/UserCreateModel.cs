using System.ComponentModel.DataAnnotations;

namespace Web_BanHang_T6S34.Models
{
    public class UserCreateModel
    {
        [Required(ErrorMessage = "Email là bắt buộc.")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mật khẩu là bắt buộc.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string[] Roles { get; set; }

        public IFormFile? Avatar { get; set; }
    }
}