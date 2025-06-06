namespace Web_BanHang_T6S34.Models.Repositories
{
    public interface ICartRepository
    {
        Task<Cart> GetCartByUserId(string userId);
        Task AddCart(Cart cart);
        Task UpdateCart(Cart cart);
        Task DeleteCart(int cartId);
    }
}