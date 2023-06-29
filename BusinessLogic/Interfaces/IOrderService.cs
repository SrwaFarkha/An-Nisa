using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.OrderModels;

namespace BusinessLogic.Interfaces
{
	public interface IOrderService
	{
		Task<OrderDto> GetOrderDetailsByAccountId(int accountId);
		Task<List<OrderDto>> GetAllOrderDetails();
		Task CreateOrder(NewOrderInputModel model);
	}
}
