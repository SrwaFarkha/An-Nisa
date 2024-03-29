﻿using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
	public class Product
	{
		[Key]
		public int ProductId { get; set; }
		public string ProductName { get; set; }
		public string ProductDescription { get; set; }
		public decimal Price { get; set; }
		public int CategoryId { get; set; }
		public bool Discontinued { get; set; }
		public virtual Category Category { get; set; }

	}
}
