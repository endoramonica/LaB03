namespace Web_BanHang_T6S34.Models.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAll();
        Task<Product> GetById(int id);
        Task Create (Product product);
        Task Update (Product product);
        Task Delete (int id);
    }
}
