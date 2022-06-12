using System.ComponentModel.DataAnnotations;

namespace BikeStore.Dtos;

public class ProductDto
{
    [Required] public int ProductId { get; set; }
    [Required] public string ProductName { get; set; } = null!;
    [Required] public int BrandId { get; set; }
    [Required] public int CategoryId { get; set; }
    public short ModelYear { get; set; }
    [Required] public decimal ListPrice { get; set; }
    
    [Required] public string BrandName { get; set; } = null!;
    [Required] public string CategoryName { get; set; } = null!;

    public static ProductDto FromEntity(Product product)
    {
        return new ProductDto()
        {
            ProductId = product.ProductId,
            ProductName = product.ProductName,
            BrandId = product.BrandId,
            CategoryId = product.CategoryId,
            ModelYear = product.ModelYear,
            ListPrice = product.ListPrice,
            
            BrandName = product.Brand.BrandName,
            CategoryName = product.Category.CategoryName
        };
    }
}