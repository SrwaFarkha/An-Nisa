using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.OrderModels
{
	public class NewOrderInputModel
	{
		public int AccountId { get; set; }
		public DateTime OrderDate { get; set; }

		public IEnumerable<OrderDetailsInputModel> OrderDetails { get; set; }
	}
}
