using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
	public class SizeStock
	{
		public int SizeStockId { get; set; }
		public string Size { get; set; }
		public int StockBalance { get; set; }

		public int ProductId { get; set; }
		public Product Product { get; set; }
	}
}
