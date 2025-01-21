using DatabaseModels.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels.ProductModels
{
	public class SizeStockDto
	{
		public int SizeStockId { get; set; }
		public Enums.Size Size { get; set; }
		public int StockBalance { get; set; }
	}
}
