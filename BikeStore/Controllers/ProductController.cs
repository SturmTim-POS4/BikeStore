using BikeStore.Dtos;
using BikeStore.Services;

namespace BikeStore.Controllers;

[Route("[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private BikeStoreService _bikeStoreService;
    public ProductController(BikeStoreService bikeStoreService)
    {
        _bikeStoreService = bikeStoreService;
    }
    
    [HttpGet("{storeId}")]
    public List<ProductDto> GetProductsOfStore(int storeId)
    {
        return _bikeStoreService.GetAllStocks().Where(x => x.Store.StoreId == storeId)
            .Select(x => ProductDto.FromEntity(x.Product))
            .ToList();
    }
}