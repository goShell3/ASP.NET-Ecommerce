using Ecommerce.Domain.Models;
using Ecommrce.Domain.Common;

namespace Ecommerce.Domain.Common.Models
{
    public abstract class AggregateRoot<TId> : Entity<TId> where TId : ValueObject
    {
        protected AggregateRoot(TId id) : base(id)
        {            
        }
    }
}