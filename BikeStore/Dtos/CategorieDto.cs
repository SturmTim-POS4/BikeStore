using System.ComponentModel.DataAnnotations;

namespace BikeStore.Dtos;

public class CategoryDto
{
    [Required] public int CategoryId { get; set; }
    [Required] public string CategoryName { get; set; } = null!;
}