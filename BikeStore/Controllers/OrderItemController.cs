using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BikeStore.Dtos;
using BikeStore.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BikeStore.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private BikeStoreService _bikeStoreService;
        public OrderItemController(BikeStoreService bikeStoreService)
        {
            _bikeStoreService = bikeStoreService;
        }
        
        [HttpPost]
        public OrderItemDto PostOrderItem([FromBody] OrderItemDto orderItemDto)
        {
            OrderItem newOrderItem = _bikeStoreService.InsertOrderItem(new OrderItem().CopyPropertiesFrom(orderItemDto));
            return new OrderItemDto().CopyPropertiesFrom(newOrderItem);
        }
        
        [HttpGet("{orderId}")]
        public List<OrderItemDto> GetOrderItems(int orderId)
        {
            return _bikeStoreService.GetAllOrderItems().Where(x => x.OrderId == orderId).Select(x => new OrderItemDto().CopyPropertiesFrom(x)).ToList();
        }
    }
}