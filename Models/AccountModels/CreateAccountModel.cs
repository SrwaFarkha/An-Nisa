using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.AccountModels
{
	public class CreateAccountModel
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string PhoneNumber { get; set; }
		public DateTime CreatedOn { get; set; }
		public string Password { get; set; }
		public AddressDto Address { get; set; }
	}
}

