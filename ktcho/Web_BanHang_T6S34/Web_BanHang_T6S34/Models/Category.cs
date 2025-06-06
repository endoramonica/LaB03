namespace Web_BanHang_T6S34.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        //Kết nối với bảng Product
        public List<Product>? Products { get; set; }
    }
}
