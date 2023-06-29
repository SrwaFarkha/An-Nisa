using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
	public class ShoppingCart
	{
		public int Id { get; set; }
		public int AccountId { get; set; }
		public Account Account { get; set; }
		public ICollection<CartItem> CartItems { get; set; } 

	}
}
