using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace DataAccess.Models
{
	public class Product
	{
		[Key]
		public int ProductId { get; set; }
		public string ProductName { get; set; }
		public decimal Price { get; set; }
		public int CategoryId { get; set; }

		public bool Discontinued { get; set; }
		public virtual Category Category { get; set; }
		public ICollection<SizeStock> Sizes { get; set; }

		public ICollection<Comment>? Comments { get; set; }
		public ICollection<CartItem> CartItems { get; set; }
		public ICollection<Image> Images { get; set; }

		public ProductDetails? ProductDetails { get; set; }





	}
}
