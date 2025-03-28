using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Dtos.Order;
using BookStore.Extensions;
using BookStore.Interfaces;
using BookStore.Mappers;
using BookStore.Models;
using BookStore.Models.ResponeApi;
using CloudinaryDotNet.Actions;
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
        private readonly IShoppingCartRepository _shoppingCart;

        public OrderController(IOrderRepository orderRepo, IOrderDetailRepository orderDetailRepo, IShoppingCartRepository shoppingCart)
        {
            _orderRepo = orderRepo;
            _orderDetailRepo = orderDetailRepo;
            _shoppingCart = shoppingCart;
        }

        [HttpGet]
        public async Task<IActionResult> Get() {
            var userId = User.GetUserId();
            var orders = await _orderRepo.GetAsync(userId);

            var lsOrderDto = new List<OrderDto>();

            foreach(var order in orders) {
                var orderDetail = await _orderDetailRepo.GetByOrderIdAsync(order.Id);
                lsOrderDto.Add(order.ToOrderDto(orderDetail));
            }

            return Ok(new ApiResponse<List<OrderDto>>(200, lsOrderDto));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id) {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.GetUserId();
            var order = await _orderRepo.GetByIdAsync(userId, id);

            if(order == null)
                return NotFound();

            var orderDetails = await _orderDetailRepo.GetByOrderIdAsync(order.Id);
            
            return Ok(new ApiResponse<OrderDto>(200, order.ToOrderDto(orderDetails)));
        }
        
        // [HttpPost("now")]
        // public async Task<IActionResult> CreateNow([FromBody] CreateOrderNowDto orderDto) {
        //     if(!ModelState.IsValid)
        //         return BadRequest(new ApiResponse<string>(400, null, "Request data is wrong structure.", false));

        //     var userId = User.GetUserId();
        //     var order = orderDto.ToOrderFromDto(userId);
        //     order.Status = order.Status.ToUpper();
        //     order.PaymentMethod = order.PaymentMethod.ToUpper();
        //     await _orderRepo.CreateAysnc(order);

        //     var orderDetailDto = orderDto.OrderDetail;
        //     var orderDetail = orderDetailDto.ToOderDetailFromDto(order.Id);
        //     await _orderDetailRepo.CreateAsync(orderDetail);

        //     var totalPrice = orderDetail.Quantity * orderDetail.PriceAtPurchase;
        //     await _orderRepo.UpdateTotalPriceAsync(order.Id, totalPrice);

        //     var newOrder = await _orderRepo.GetByIdAsync(userId, order.Id);


        //     return CreatedAtAction(nameof(GetById), new {id = newOrder.Id}, new ApiResponse<OrderDto>(201, newOrder.ToOrderDto(new List<OrderDetail>() {orderDetail})));
        // }

        // [HttpPost]
        // public async Task<IActionResult> CreateFromShippingCart([FromBody]CreateOrderDto createOrder) {
        //     if(!ModelState.IsValid)
        //         return BadRequest(new ApiResponse<string>(400, null, "Request data is wrong structure.", false));
            
        //     var userId = User.GetUserId();

        //     var shoppingCarts = await _shoppingCart.GetAllByUserIdAsync(userId);
        //     if(shoppingCarts.Count == 0)
        //         return BadRequest(new ApiResponse<string>(400, null, "Your shopping cart is null.", false));

        //     var order = createOrder.ToOderFromDto(userId);
        //     order.Status = order.Status.ToUpper();
        //     order.PaymentMethod = order.PaymentMethod.ToUpper();
        //     await _orderRepo.CreateAysnc(order);

        //     var orderDetails = shoppingCarts.Select(sc => sc.ToOrderDetailFromShoppingCart(order.Id)).ToList();
        //     await _orderDetailRepo.CreateAsync(orderDetails);
        //     await _shoppingCart.ClearAsync(userId);

        //     var totalPrice = orderDetails.Sum(od => od.PriceAtPurchase*od.Quantity);

        //     await _orderRepo.UpdateTotalPriceAsync(order.Id, totalPrice);

        //     var newOrder = await _orderRepo.GetByIdAsync(userId, order.Id);
        //     return CreatedAtAction(nameof(GetById), new {id = newOrder.Id}, newOrder.ToOrderDto());
        // }

        // [HttpPatch("{orderId}")]
        // public async Task<IActionResult> UpdateAddress([FromRoute] Guid orderId, [FromBody] UpdateOrderAddressDto updateOrderAddressDto) {
        //     if(!ModelState.IsValid)
        //         return BadRequest(new ApiResponse<string>(400, null, "Request data is wrong structure.", false));
            
        //     var userId = User.GetUserId();

        //     var order = await _orderRepo.GetByIdAsync(userId, orderId);
        //     if(order == null)
        //         return NotFound(new ApiResponse<string>(404, null, "Couldn't find the order.", false));

        //     if(order.Status.ToUpper() != "PREPARE") {
        //         return BadRequest(new ApiResponse<string>(400, null, "Your order was shipped so you couldn't update your address.", false));
        //     }

        //     var newOrder = await _orderRepo.UpdateShippingAddress(order.Id, updateOrderAddressDto.ShippingAddress);

        //     return Ok(new ApiResponse<OrderDto>(200, newOrder.ToOrderDto()));
        // }
    
        // [HttpPatch("cancel/{orderId}")]
        // [Authorize(Roles = "CUSTOMER")]
        // public async Task<IActionResult> UpdateState([FromRoute] Guid orderId, [FromBody] UpdateOrderStatusDto updateOrderStatusDto) {
        //     var userId = User.GetUserId();

        //     var order = await _orderRepo.GetByIdAsync(userId, orderId);
        //     if(order == null)
        //         return NotFound(new ApiResponse<string>(404, null, "Couldn't find the order.", false));
            
        //     if(order.Status.ToUpper() != "PREPARE" && order.Status.ToUpper() != "CREATED") {
        //         return BadRequest(new ApiResponse<string>(400, null, "Your order was already sent so you couldn't cancel it.", false));
        //     }

        //     var orderUpdated = await _orderRepo.UserCancelOrderAsync(orderId, updateOrderStatusDto.CancelReason);

        //     return Ok(new ApiResponse<OrderDto>(200, orderUpdated.ToOrderDto()));
        // }

        // [HttpPatch("update-status/{orderId}")]
        // [Authorize(Roles = "ADMIN, STAFF")]
        // public async Task<IActionResult> UpdateStatus([FromRoute] Guid orderId, [FromBody] StaffUpdateOrderStatusDto updateOrderStatusDto) {
        //     if(!ModelState.IsValid)
        //         return BadRequest(new ApiResponse<string>(400, null, "Request data is wrong structure.", false));
            
        //     string[] statusList = {"CREATED", "PREPARING", "SHIPPING", "COMPLETED", "CANCELING", "CANCELED"};
        //     if(!statusList.Contains(updateOrderStatusDto.Status)) 
        //         return BadRequest(new ApiResponse<string>(400, null, "Status have to in list [\"CREATED\", \"PREPARING\", \"SHIPPING\", \"COMPLETED\", \"CANCELING\", \"CANCELED\"]", false));

        //     var userId = User.GetUserId();
        //     var order = await _orderRepo.GetByIdAsync(orderId);
        //     if(order == null)
        //         return NotFound(new ApiResponse<string>(404, null, "Couldn't find the order.", false));

        //     var orderUpdated = await _orderRepo.UpdateOrderStatusAsycn(order.Id, updateOrderStatusDto);

        //     return Ok(new ApiResponse<OrderDto>(200, orderUpdated.ToOrderDto())); 
        // }
    }
}