namespace OnlineShop.Shared.DTOs
{
    public class ProductsListDto
    {
        public List<ProductDto> Products { get; set; }
        public int TotalItems { get; set; }
    }
}