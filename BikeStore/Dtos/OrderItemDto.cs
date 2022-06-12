using Microsoft.Build.Framework;

namespace BikeStore.Dtos;

public class OrderItemDto
{
    [Required] public int OrderId { get; set; }
    [Required] public int ItemId { get; set; }
    [Required] public int ProductId { get; set; }
    [Required] public int Quantity { get; set; }
    [Required] public decimal ListPrice { get; set; }
}