using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using Web_BanHang_T6S34.Models;
using Web_BanHang_T6S34.Models.RegisterLogin;


namespace Web_BanHang_T6S34.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ProfileController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }
            var model = new ProfileViewModel
            {
                Email = user.Email,
                AvatarUrl = user.AvatarUrl ?? "/images/default-avatar.jpg"
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAvatar(ProfileViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            if (model.Avatar != null && model.Avatar.Length > 0)
            {
                // Xóa avatar cũ nếu có
                if (!string.IsNullOrEmpty(user.AvatarUrl) && user.AvatarUrl != "/images/default-avatar.jpg")
                {
                    var oldAvatarPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", user.AvatarUrl.TrimStart('/'));
                    if (System.IO.File.Exists(oldAvatarPath))
                    {
                        System.IO.File.Delete(oldAvatarPath);
                    }
                }

                // Upload avatar mới
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.Avatar.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/avatars", fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.Avatar.CopyToAsync(stream);
                }
                user.AvatarUrl = $"/avatars/{fileName}";
                await _userManager.UpdateAsync(user);
            }

            return RedirectToAction("Index");
        }
    }
}