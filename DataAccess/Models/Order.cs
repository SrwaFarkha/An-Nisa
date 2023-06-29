using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
	public class Order
	{
		public int OrderId { get; set; }

		public DateTime OrderDate { get; set; }

		[ForeignKey("Account")]
		public int AccountId { get; set; }
		public virtual Account Account { get; set; }

		public virtual ICollection<OrderDetails> OrderDetails { get; set; }
	}
}
