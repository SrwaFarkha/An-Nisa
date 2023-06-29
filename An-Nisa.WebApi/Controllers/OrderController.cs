using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.OrderModels;

namespace An_Nisa.WebApi.Controllers
{
	[Route("api/order")]
	[ApiController]
	public class OrderController : ControllerBase
	{
		private readonly IOrderService _orderService;

		public OrderController(IOrderService orderService)
		{
			_orderService = orderService;
		}

		[HttpGet("{accountId:int}/get-orderdetails-by-accountid")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> GetOrderDetailsByAccountId(int accountId)
		{
			var result = await _orderService.GetOrderDetailsByAccountId(accountId);
			return Ok(result);
		}

		[HttpGet("get-all-order-details")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> GetAllOrderDetails()
		{
			var result = await _orderService.GetAllOrderDetails();
			return Ok(result);
		}

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> CreateOrder(NewOrderInputModel newOrder)
		{
			await _orderService.CreateOrder(newOrder);

			return Ok();
		}


	}
}
