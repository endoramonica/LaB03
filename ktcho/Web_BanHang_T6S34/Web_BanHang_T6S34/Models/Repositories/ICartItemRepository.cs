namespace Web_BanHang_T6S34.Models.Repositories
{
    public interface ICartItemRepository
    {
        Task AddCartItem(CartItem cartItem);
        Task UpdateCartItem(CartItem cartItem);
        Task DeleteCartItem(int cartItemId);
    }
}