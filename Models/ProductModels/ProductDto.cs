using Models.ImageModels;
using Models.ProductDetailsModels;

namespace Models.ProductModels
{
    public class ProductDto
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public string CategoryName { get; set; }
        public bool Discontinued { get; set; }
        public IEnumerable<SizeStockDto> Sizes { get; set; }

		public IEnumerable<ImageDto> Images { get; set; }
        public ProductDetailsDto? ProductDetails { get; set; }
    }
}
