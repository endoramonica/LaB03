using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web_BanHang_T6S34.Models;
using Web_BanHang_T6S34.Models.Repositories;

namespace Web_BanHang_T6S34.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository _productRepository;
        private ICategoryRepository _categoryRepository;
        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<IActionResult> Index()
        {
            var product = await _productRepository.GetAll();
            return View("Index",product);
        }

        //Tạo form để điền dữ liệu
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            var list_Category = await _categoryRepository.GetAll();
            ViewBag.Categories = new SelectList(list_Category, "Id", "Name");

            return View();
        }

        //Khi nhấn OK để tạo sản phẩm mới
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(Product product, IFormFile? file_Image)
        {
            if (ModelState.IsValid)
            {
                // Lưu hình ảnh
                if (file_Image != null && file_Image.Length > 0)
                {
                    string path = "wwwroot/images";
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    string filename = Guid.NewGuid() + Path.GetExtension(file_Image.FileName);
                    path = Path.Combine(path, filename);
                    using (FileStream fs = new FileStream(path, FileMode.Create))
                    {
                        await file_Image.CopyToAsync(fs);
                    }

                    product.ImageUrl = "images/" + filename;
                }

                // Gọi lưu vào DB
                await _productRepository.Create(product);
                return RedirectToAction("Index", "Product");
            }

            // Nếu có lỗi thì load lại danh sách category cho dropdown
            var list_Category = await _categoryRepository.GetAll();
            ViewBag.Categories = new SelectList(list_Category, "Id", "Name");
            return View(product);
        }

        // Chỉnh sửa sản phẩm
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productRepository.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            var list_Category = await _categoryRepository.GetAll();
            ViewBag.Categories = new SelectList(list_Category, "Id", "Name", product.CategoryId);
            return View(product);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(Product product, IFormFile? file_Image)
        {
            if (ModelState.IsValid)
            {
                if (file_Image != null && file_Image.Length > 0)
                {
                    string path = "wwwroot/images";
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    string filename = Guid.NewGuid() + Path.GetExtension(file_Image.FileName);
                    path = Path.Combine(path, filename);
                    using (FileStream fs = new FileStream(path, FileMode.Create))
                    {
                        await file_Image.CopyToAsync(fs);
                    }

                    product.ImageUrl = "images/" + filename;
                }

                await _productRepository.Update(product);
                return RedirectToAction("Index");
            }

            var list_Category = await _categoryRepository.GetAll();
            ViewBag.Categories = new SelectList(list_Category, "Id", "Name", product.CategoryId);
            return View(product);
        }

        // Xóa sản phẩm
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productRepository.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            await _productRepository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
