﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.AccountModels
{
	public class AddProductToShoppingCartModel
	{
		public int AccountId { get; set; }
		public int ProductId { get; set; }
		public int Quantity { get; set; }
	}
}
