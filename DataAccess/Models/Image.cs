using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
	public class Image
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Url { get; set; }

		public int ProductId { get; set; } 
		public Product Product { get; set; }
	}
}
