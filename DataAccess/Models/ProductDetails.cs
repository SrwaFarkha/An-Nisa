using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
	public class ProductDetails
	{
		public int Id { get; set; }
		public string Material { get; set; }
		public string Fabric { get; set; }

		public string CareAdvice { get; set; }
		public string Belt { get; set; }
		public string Zipper { get; set; }
		public string ArticleNumber { get; set; }
		public string OurModel { get; set; }
		public string Fit { get; set; }
		public string Arm { get; set; }
		public string Lenght { get; set; }
		public string ModelSize { get; set; }
		public string Details { get; set; }
		public string Color { get; set;}

		public int ProductId { get; set; } 
		public Product Product { get; set; } = null!;



	}
}
