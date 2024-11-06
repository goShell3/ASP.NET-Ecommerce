using Ecommerce.Domain.Models;

namespace EcommerceDomain.Product.Entities
{
    public class Stock : Entity<StockId>
    {
       

        public string? available{ get; private set; }
        public int quantity { get; private set; }
        
         public Stock(StockId entityId) : base(entityId)
        {

        }
    }
}