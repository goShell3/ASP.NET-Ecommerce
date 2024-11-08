using Ecoimmerce.Domian.Product.ValueObjects;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Models;

namespace EcommerceDomain.Order.Entities
{
    public class OrderItem
    {
        public ProductId productId {get; set;}
        public int Quantity { get; private set; }



        public OrderItem(ProductId product, int quantity)
        {
            productId = product ?? throw new ArgumentNullException(nameof(product));
            Quantity = quantity > 0 ? quantity : throw new ArgumentException("Quantity must be greater than zero.");
        }
    }
}