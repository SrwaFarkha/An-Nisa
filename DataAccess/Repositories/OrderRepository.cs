using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Interfaces;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace DataAccess.Repositories
{
	public class OrderRepository : IOrderRepository
	{
		private readonly DataContext.AnContext _dbContext;

		public OrderRepository(DataContext.AnContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<Order> GetOrderDetailsByAccountId(int accountId)
		{
			var order = await _dbContext.Orders
				.Include(x => x.Account)
				.ThenInclude(x => x.Address)
				.Include(x => x.OrderDetails)
				.ThenInclude(x => x.Product)
				.ThenInclude(x => x.Category)
				.Where(x => x.AccountId == accountId)
				.FirstOrDefaultAsync();

			return order;
		}

		public async Task<List<Order>> GetAllOrderDetails()
		{
			var order = await _dbContext.Orders
				.Include(x => x.Account)
				.ThenInclude(x => x.Address)
				.Include(x => x.OrderDetails)
				.ThenInclude(x => x.Product)
				.ThenInclude(x => x.Category)
				.ToListAsync();

			return order;
		}

		public async Task CreateOrder(Order newOrder)
		{
			await _dbContext.Orders.AddAsync(newOrder);
			await _dbContext.SaveChangesAsync();
		}
	}
}
