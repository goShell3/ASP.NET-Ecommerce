using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Application.DTOs
{
    public class ProductDto
    {
        [Required]
        public string? Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string? Description { get; set; }

        public int StockQuantity { get; set; }
    }
}
