namespace Web_BanHang_T6S34.Models.Repositories
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetOrdersByUserId(string userId);
        Task<Order> GetOrderById(int orderId);
        Task AddOrder(Order order);
    }
}