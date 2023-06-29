using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Models.AccountModels
{
	public class AddressDto
	{
		//public int AddressId { get; set; }
		public string Country { get; set; }
		public string City { get; set; }
		public string PostNumber { get; set; }
		public string StreetAddress { get; set; }
		//public ICollection<AccountDto> Accounts { get; set; }
	}
}
