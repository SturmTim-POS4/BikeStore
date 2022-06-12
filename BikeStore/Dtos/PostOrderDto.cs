namespace BikeStore.Dtos;

public class PostOrderDto
{
    public int? CustomerId { get; set; }
    public int StoreId { get; set; }
    public int StaffId { get; set; }
    public byte OrderStatus { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime RequiredDate { get; set; }
}