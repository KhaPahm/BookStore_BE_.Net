using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Dtos.Order;
using BookStore.Extensions;
using BookStore.Interfaces;
using BookStore.Mappers;
using BookStore.Models.ResponeApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers.V1
{
    [Route("api/v1/order")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepo;
        private readonly IOrderDetailRepository _orderDetailRepo;

        public OrderController(IOrderRepository orderRepo, IOrderDetailRepository orderDetailRepo)
        {
            _orderRepo = orderRepo;
            _orderDetailRepo = orderDetailRepo;
        }

        [HttpGet]
        public async Task<IActionResult> Get() {
            var userId = User.GetUserId();
            var orders = await _orderRepo.GetAsync(userId);

            var orderDtos = orders.Select(o => o.ToOrderDto()).ToList();
            
            return Ok(orderDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id) {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.GetUserId();
            var order = await _orderRepo.GetByIdAsync(userId, id);

            if(order == null)
                return NotFound();
            
            return Ok(order.ToOrderDto());
        }
        

        // [HttpPost]
        // public async Task<IActionResult> Create([FromBody] CreateOrderDto orderDto) {
        //     if(ModelState.IsValid == false)
        //         return BadRequest(ModelState);

        //     var userId = User.GetUserId();
        //     var order = orderDto.ToOderFromDto(userId);

        //     await _orderRepo.CreateAysnc(order);

        //     var orderDetails = orderDto.OrderDetailDtos
        //         .Select(od => od.ToOderDetailFromDto(order.Id))
        //         .ToList();

        //     await _orderDetailRepo.CreateAsync(orderDetails);
            
        //     var totalPrice = orderDetails.Sum(od => od.PriceAtPurchase);

        //     await _orderRepo.UpdateTotalPriceAsync(order.Id, totalPrice);

        //     var newOrder = await _orderRepo.GetByIdAsync(userId, order.Id);

        //     return CreatedAtAction(nameof(GetById), new {id = newOrder.Id}, newOrder.ToOrderDto());
        // }

        [HttpPost("now")]
        public async Task<IActionResult> CreateNow([FromBody] CreateOrderNowDto orderDto) {
            if(!ModelState.IsValid)
                return BadRequest(new ApiResponse<string>(400, null, "Request data is wrong structure.", false));

            var userId = User.GetUserId();
            var order = orderDto.ToOrderFromDto(userId);
            order.Status = order.Status.ToUpper();
            order.PaymentMethod = order.PaymentMethod.ToUpper();
            await _orderRepo.CreateAysnc(order);

            var orderDetailDto = orderDto.OrderDetail;
            var orderDetail = orderDetailDto.ToOderDetailFromDto(order.Id);
            await _orderDetailRepo.CreateAsync(orderDetail);

            var totalPrice = orderDetail.Quantity * orderDetail.PriceAtPurchase;
            await _orderRepo.UpdateTotalPriceAsync(order.Id, totalPrice);

            var newOrder = await _orderRepo.GetByIdAsync(userId, order.Id);
            return CreatedAtAction(nameof(GetById), new {id = newOrder.Id}, new ApiResponse<OrderDto>(201, newOrder.ToOrderDto()));
        }
    }
}