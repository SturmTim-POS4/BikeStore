using System.ComponentModel.DataAnnotations;

namespace BikeStore.Dtos;

public class StoreDto
{
    [Required] public int StoreId { get; set; }
    [Required] public string StoreName { get; set; } = null!;
}