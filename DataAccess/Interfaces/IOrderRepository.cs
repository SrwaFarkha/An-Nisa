using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;

namespace DataAccess.Interfaces
{
	public interface IOrderRepository
	{
		Task<Order> GetOrderDetailsByAccountId(int accountId);
		Task<List<Order>> GetAllOrderDetails();
		Task CreateOrder(Order newOrder);
	}
}
