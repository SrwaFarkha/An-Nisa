using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
	public class Address
	{
		public int AddressId { get; set; }
		public string Country { get; set; }
		public string City { get; set; }
		public string PostNumber { get; set; }
		public string StreetAddress { get; set; }
		public ICollection<Account> Accounts { get; set; }


	}
}
