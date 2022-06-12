using BikeStore.Dtos;
using BikeStore.Services;

namespace BikeStore.Controllers;

[Route("[controller]")]
[ApiController]
public class OrderController
{
    private BikeStoreService _bikeStoreService;
    public OrderController(BikeStoreService bikeStoreService)
    {
        _bikeStoreService = bikeStoreService;
    }
    
    [HttpPost]
    public OrderDto PostOrder([FromBody] PostOrderDto postOrderDto)
    {
        Order newOrder = _bikeStoreService.InsertOrder(new Order().CopyPropertiesFrom(postOrderDto));
        return new OrderDto().CopyPropertiesFrom(newOrder);
    }
    
    [HttpGet]
    public List<OrderDto> GetOrders()
    {
        return _bikeStoreService.GetAllOrders().Select(x => new OrderDto().CopyPropertiesFrom(x)).ToList();
    }
}