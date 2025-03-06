using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels.ShoppingCartModels
{
    public class ShoppingCartDto
    {
        public IEnumerable<CartItemDto>? Products { get; set; }
        public decimal ShoppingCartTotalPrice { get; set; }
    }
}
