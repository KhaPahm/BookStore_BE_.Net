using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Dtos.Order;
using BookStore.Exceptions;
using BookStore.Interfaces;
using BookStore.Interfaces.Services;
using BookStore.Mappers;
using BookStore.Static;

namespace BookStore.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepo;
        private readonly IOrderDetailRepository _orderDetailRepo;
        private readonly IShoppingCartRepository _shoppingCart;
        private readonly IPaypalService _paypalService;

        public OrderService(IOrderRepository orderRepo, IOrderDetailRepository orderDetailRepo, IShoppingCartRepository shoppingCart, IPaypalService paypalService)
        {
            _orderRepo = orderRepo;
            _orderDetailRepo = orderDetailRepo;
            _shoppingCart = shoppingCart;
            _paypalService = paypalService;
        }

        public async Task<string> CreateOrderNowAsync(Guid userId, CreateOrderNowDto orderDto)
        {
            var order = orderDto.ToOrderModel(userId);
            order.PaymentMethod = order.PaymentMethod.ToUpper();
            if(order.PaymentMethod == "PAYPAL")
            {
                order.Status = OrderStatus.Paying;
            }
        
            await _orderRepo.CreateAysnc(order);

            var orderDetailDto = orderDto.OrderDetail;
            var orderDetail = orderDetailDto.ToOderDetailModel(order.Id);
            await _orderDetailRepo.CreateAsync(orderDetail);

            var totalPrice = orderDetail.Quantity * orderDetail.PriceAtPurchase;
            await _orderRepo.UpdateTotalPriceAsync(order.Id, totalPrice);

            if(order.PaymentMethod == PaymentMethods.Paypal)
            {
                var approvalUrl = await _paypalService.CreatePayment((decimal)totalPrice, PaypalExcute.Success, PaypalExcute.Cancel);
                var paypalTransactionId = approvalUrl.Split("=")[1];
                await _orderRepo.UpdatePayPalOrderId(order.Id, paypalTransactionId);
                return approvalUrl;
            }

        }

        public Task<bool> ExcutePaymentAsync(string paymentToken)
        {
            throw new NotImplementedException();
        }

        public async Task<OrderDto> GetOrderByIdAsync(Guid userId, Guid orderId)
        {
            var orders = await _orderRepo.GetByIdAsync(userId, orderId);
            if (orders == null)
                throw new NotFoundException("Order not found");

            var orderDetail = await _orderDetailRepo.GetByOrderIdAsync(orderId);

            return orders.ToOrderDto(orderDetail);
        }

        public async Task<List<OrderDto>> GetOrdersAsync(Guid userId)
        {
            var orders = await _orderRepo.GetAsync(userId);

            var lsOrderDto = new List<OrderDto>();
            foreach (var order in orders)
            {
                var orderDetail = await _orderDetailRepo.GetByOrderIdAsync(order.Id);
                lsOrderDto.Add(order.ToOrderDto(orderDetail));
            }
            return lsOrderDto;
        }
    }
}