using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseModels.DatabaseEnums;


namespace SharedModels.AccountModels
{
	public class AddProductToShoppingCartModel
	{
		public int AccountId { get; set; }
		public int ProductId { get; set; }
        public DatabaseEnums.Size Size { get; set; }
        public int Quantity { get; set; }
	}
}
