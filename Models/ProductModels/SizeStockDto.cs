using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ProductModels
{
	public class SizeStockDto
	{
		public int SizeStockId { get; set; }
		public string Size { get; set; }
		public int StockBalance { get; set; }
	}
}
