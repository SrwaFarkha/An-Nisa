using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Interfaces;
using DataAccess.Interfaces;
using DataAccess.Models;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Abstractions;
using Models.AccountModels;
using Models.OrderModels;

namespace BusinessLogic.Services
{
	public class OrderService : IOrderService
	{
		private readonly IOrderRepository _orderRepository;

		public OrderService(IOrderRepository orderRepository)
		{
			_orderRepository = orderRepository;
		}

		public async Task<OrderDto> GetOrderDetailsByAccountId(int accountId)
		{
			var orders = await _orderRepository.GetOrderDetailsByAccountId(accountId);
			var orderDto = new OrderDto
			{
				FirstName = orders.Account.FirstName,
				LastName = orders.Account.LastName,
				Email = orders.Account.Email,
				PhoneNumber = orders.Account.PhoneNumber,
				Address = new AddressDto
				{
					Country = orders.Account.Address.Country,
					City = orders.Account.Address.City,
					PostNumber = orders.Account.Address.PostNumber,
					StreetAddress = orders.Account.Address.StreetAddress,
				},
				OrderDate = orders.OrderDate,
				OrderDetails = orders.OrderDetails.Select(orderDetails => new OrderDetailsDto
				{
					ProductId = orderDetails.ProductId,
					ProductName = orderDetails.Product.ProductName,
					Quantity = orderDetails.Quantity,
					Price = orderDetails.Product.Price,
					TotalProductsPrice = orderDetails.Product.Price * orderDetails.Quantity
				}).ToList()
			};

			return orderDto;
		}

		public async Task<List<OrderDto>> GetAllOrderDetails()
		{
			var orders = await _orderRepository.GetAllOrderDetails();

			var orderDtos = orders.Select(order => new OrderDto
			{
				FirstName = order.Account.FirstName,
				LastName = order.Account.LastName,
				Email = order.Account.Email,
				PhoneNumber = order.Account.PhoneNumber,
				Address = new AddressDto
				{
					Country = order.Account.Address.Country,
					City = order.Account.Address.City,
					PostNumber = order.Account.Address.PostNumber,
					StreetAddress = order.Account.Address.StreetAddress,
				},
				OrderDate = order.OrderDate,
				OrderDetails = order.OrderDetails.Select(orderDetails => new OrderDetailsDto
				{
					ProductId = orderDetails.ProductId,
					ProductName = orderDetails.Product.ProductName,
					Quantity = orderDetails.Quantity,
					Price = orderDetails.Product.Price,
					TotalProductsPrice = orderDetails.Product.Price * orderDetails.Quantity
				}).ToList()
			}).ToList();

			return orderDtos;

		}

		public async Task CreateOrder(NewOrderInputModel model)
		{
			var newOrder = new Order
			{
				OrderDate = model.OrderDate,
				AccountId = model.AccountId,
				OrderDetails = model.OrderDetails?.Select(orderDetails => new OrderDetails
				{
					ProductId = orderDetails.ProductId,
					Quantity = orderDetails.Quantity
				}).ToList()
			};

			await _orderRepository.CreateOrder(newOrder);

		}
	}
}
