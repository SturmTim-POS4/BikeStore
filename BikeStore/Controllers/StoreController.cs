using BikeStore.Dtos;
using BikeStore.Services;

namespace BikeStore.Controllers;

[Route("[controller]")]
[ApiController]
public class StoreController : ControllerBase
{
    private BikeStoreService _bikeStoreService;
    public StoreController(BikeStoreService bikeStoreService)
    {
        _bikeStoreService = bikeStoreService;
    }
    
    [HttpGet]
    public List<StoreDto> GetStores()
    {
        return _bikeStoreService.GetAllStores().Select(x => new StoreDto().CopyPropertiesFrom(x)).ToList();
    }
}