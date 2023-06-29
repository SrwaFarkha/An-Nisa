using Models.AccountModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.OrderModels
{
	public class OrderDto
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string PhoneNumber { get; set; }
		public AddressDto Address { get; set; }
		public DateTime OrderDate { get; set; }
		public List<OrderDetailsDto> OrderDetails { get; set; }
	}
}
