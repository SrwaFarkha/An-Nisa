﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ProductModels
{
	public class CreateCommentModel
	{
		public string Name { get; set; }
		public string Text { get; set; }
		public int Rating { get; set; }
	}
}
