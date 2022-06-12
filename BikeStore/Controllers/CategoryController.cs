using BikeStore.Dtos;
using BikeStore.Services;

namespace BikeStore.Controllers;

[Route("[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private BikeStoreService _bikeStoreService;
    public CategoryController(BikeStoreService bikeStoreService)
    {
        _bikeStoreService = bikeStoreService;
    }
    
    [HttpGet("{storeId}")]
    public List<CategoryDto> GetCategoriesOfStore(int storeId)
    {
        var storeCategories = _bikeStoreService.GetAllStores()
            .First(x => x.StoreId == storeId)
            .Stocks
            .Select(x => x.Product.Category)
            .ToList();

        return _bikeStoreService.GetAllCategories()
            .Where(x => storeCategories.Contains(x))
            .Select(x => new CategoryDto().CopyPropertiesFrom(x))
           .ToList();
       
    }
}