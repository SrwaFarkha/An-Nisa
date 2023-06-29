﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
	public class CartItem
	{
		public int Id { get; set; }

		public Product Product { get; set; }
		public int ProductId { get; set; }

		public int Quantity { get; set; }

		public ShoppingCart ShoppingCart { get; set; }

		public int ShoppingCartId { get; set; }
	}
}
