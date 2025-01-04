using SharedModels.ImageModels;

namespace SharedModels.ProductModels
{
    public class ProductDto
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal Price { get; set; }
        public string CategoryName { get; set; }
        public bool Discontinued { get; set; }
        public List<ImageDto> Images { get; set;}
        public List<SizeStockDto> SizeStocks { get; set; }
    }
}
