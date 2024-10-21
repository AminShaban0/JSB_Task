using AutoMapper;
using JSB_Task.Dtos;
using JSB_Task.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JSB_Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderControler : ControllerBase
    {
        private readonly IGenaricRepository<Order> _orderRepo;
        private readonly IMapper _mapper;
        private readonly IGenaricRepository<Product> _productRepo;

        public OrderControler(IGenaricRepository<Order> orderRepo, IMapper mapper , IGenaricRepository<Product> productRepo)
        {
            _orderRepo = orderRepo;
            _mapper = mapper;
            _productRepo = productRepo;
        }

        [HttpPost]
        public async Task<ActionResult> CreateOrder([FromQuery] OrderDto orderDto)
        {
            if (orderDto == null)
                return BadRequest("Invalid order data.");

            var order = new Order
            {
                CustomerId = orderDto.CustomerId,
                OrderDate = DateTime.UtcNow,
            };

            foreach (var productId in orderDto.ProductId)
            {
                var orderProduct = new OrderProduct
                {
                    ProductId = productId,
                    OrderId = order.Id
                };
                order.OrderProducts.Add(orderProduct);
            }
            var products = await _productRepo.GetAllAsync();
            var productPrice = products.Where(x=>orderDto.ProductId.Contains(x.Id)).Select(x=>x.Price).ToList();
            productPrice.ForEach(x => order.TotalAmount += x);

            await _orderRepo.AddAsync(order);

            return Ok("Order created successfully.");
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetAllOrders()
        {
            var orders = await _orderRepo.GetAllAsync();
            var orderDtos = _mapper.Map<IEnumerable<OrderReturnDto>>(orders);
            return Ok(orderDtos);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetOrderById(int id)
        {
            var order = await _orderRepo.GetByIdAsync(id);
            if (order == null)
                return NotFound("Order not found.");

            var orderDto = _mapper.Map<OrderReturnDto>(order);
            return Ok(orderDto);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateOrder(int id, [FromQuery] OrderDto orderDto)
        {
            if (orderDto == null)
                return BadRequest("Invalid order data.");

            var existingOrder = await _orderRepo.GetByIdAsync(id);
            if (existingOrder == null)
                return NotFound("Order not found.");

            existingOrder.CustomerId = orderDto.CustomerId;
            existingOrder.OrderDate = DateTime.UtcNow;
            existingOrder.OrderProducts.Clear();
            foreach (var productId in orderDto.ProductId)
            {
                var orderProduct = new OrderProduct
                {
                    ProductId = productId,
                    OrderId = existingOrder.Id
                };
                existingOrder.OrderProducts.Add(orderProduct);
            }

            var products = await _productRepo.GetAllAsync();
            var productPrices = products.Where(x => orderDto.ProductId.Contains(x.Id)).Select(x => x.Price).ToList();
            existingOrder.TotalAmount = productPrices.Sum();

            _orderRepo.Update(existingOrder);

            return Ok("Order updated successfully.");
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOrder(int id)
        {
            var existingOrder = await _orderRepo.GetByIdAsync(id);
            if (existingOrder == null)
                return NotFound("Order not found.");

            _orderRepo.Delete(existingOrder);
            return Ok("Order deleted successfully.");
        }
    }
}
