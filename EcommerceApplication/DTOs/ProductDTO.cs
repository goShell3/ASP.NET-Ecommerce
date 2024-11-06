using System.ComponentModel.DataAnnotations;
using EcommerceDomain.Product.Entities;

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

        public string? Currency {get; set;}
        public Category? category {get; set;}
        public  string? Brand {get; set;}
        public List<Stock>? stocks {get; set;}
        public List<Specifications>? specifications {get; set;}
        public ProductImage? productImage {get; set;}






    }
}
