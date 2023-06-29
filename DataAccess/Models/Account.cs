using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
	public class Account
	{
		public int AccountId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string PhoneNumber { get; set; }
		public DateTime CreatedOn { get; set; }
		public bool IsAdmin { get; set; }
		public string Password { get; set; }
		public Address Address { get; set; }
		public ShoppingCart? ShoppingCart { get; set; }



	}
}
