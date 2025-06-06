using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Web_BanHang_T6S34.Models;
using Web_BanHang_T6S34.Models.Repositories;

namespace Web_BanHang_T6S34.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ICartRepository _cartRepository;
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly UserManager<IdentityUser> _userManager;

        public CartController(
            ICartRepository cartRepository,
            ICartItemRepository cartItemRepository,
            IOrderRepository orderRepository,
            IOrderDetailRepository orderDetailRepository,
            UserManager<IdentityUser> userManager)
        {
            _cartRepository = cartRepository;
            _cartItemRepository = cartItemRepository;
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var cart = await _cartRepository.GetCartByUserId(user.Id);
            return View(cart ?? new Cart { UserId = user.Id, CartItems = new List<CartItem>() });
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId, int quantity = 1)
        {
            var user = await _userManager.GetUserAsync(User);
            var cart = await _cartRepository.GetCartByUserId(user.Id);

            if (cart == null)
            {
                cart = new Cart { UserId = user.Id, CartItems = new List<CartItem>() };
                await _cartRepository.AddCart(cart);
            }

            var cartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);
            if (cartItem == null)
            {
                cartItem = new CartItem { CartId = cart.Id, ProductId = productId, Quantity = quantity };
                cart.CartItems.Add(cartItem);
                await _cartItemRepository.AddCartItem(cartItem);
            }
            else
            {
                cartItem.Quantity += quantity;
                await _cartItemRepository.UpdateCartItem(cartItem);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Checkout()
        {
            var user = await _userManager.GetUserAsync(User);
            var cart = await _cartRepository.GetCartByUserId(user.Id);

            if (cart == null || !cart.CartItems.Any())
            {
                return RedirectToAction("Index");
            }

            var order = new Order
            {
                UserId = user.Id,
                OrderDate = DateTime.Now,
                TotalAmount = cart.CartItems.Sum(ci => ci.Quantity * ci.Product.Price),
                OrderDetails = cart.CartItems.Select(ci => new OrderDetail
                {
                    ProductId = ci.ProductId,
                    Quantity = ci.Quantity,
                    Price = ci.Product.Price
                }).ToList()
            };

            await _orderRepository.AddOrder(order);
            await _cartRepository.DeleteCart(cart.Id);

            return RedirectToAction("OrderConfirmation", new { orderId = order.Id });
        }

        public async Task<IActionResult> OrderConfirmation(int orderId)
        {
            var order = await _orderRepository.GetOrderById(orderId);
            return View(order);
        }
    }
}