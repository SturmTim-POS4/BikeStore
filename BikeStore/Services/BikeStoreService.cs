using BikeStore.Dtos;

namespace BikeStore.Services;

public class BikeStoreService
{
    private readonly BikeStoreContext _db;

    public BikeStoreService(BikeStoreContext db)
    {
        _db = db;
    }

    public IEnumerable<Store> GetAllStores()
    {
        return _db.Stores.Include(x => x.Stocks).ThenInclude(x => x.Product).ThenInclude(x => x.Category).AsEnumerable();
    }
    
    public IEnumerable<OrderItem> GetAllOrderItems()
    {
        return _db.OrderItems.AsEnumerable();
    }
    
    public IEnumerable<Order> GetAllOrders()
    {
        return _db.Orders.AsEnumerable();
    }
    
    public IEnumerable<Category> GetAllCategories()
    {
        return _db.Categories.AsEnumerable();
    }
    
    public IEnumerable<Stock> GetAllStocks()
    {
        return _db.Stocks
            .Include(x => x.Product)
            .ThenInclude(x => x.Category)
            .Include(x => x.Product.Brand)
            .Include(x => x.Store).AsEnumerable();
    }

    public Order InsertOrder(Order order)
    {
        var newOrder = _db.Orders.Add(order).Entity;
        _db.SaveChanges();
        return newOrder;
    }
    
    public OrderItem InsertOrderItem(OrderItem orderItem)
    {
        var newOrderItem = _db.OrderItems.Add(orderItem).Entity;
        _db.SaveChanges();
        return newOrderItem;
    }
}