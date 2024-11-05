using System;
using Ecommrce.Domain.Common;

namespace Ecommerce.Domain.O
{
    public sealed class OrderId : ValueObject
    {
        // OrderId constructor 
        public OrderId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new ArgumentException("Order id cannot be empty", nameof(value));
            }
            Value = value;
        }

        // OrderId value
        public Guid Value { get; }

        // OrderId implicit operator
        public static implicit operator Guid(OrderId self) => self.Value;

        // OrderId implicit operator
        public static implicit operator OrderId(Guid value) => new OrderId(value);

        // OrderId ToString
        public override string ToString() => Value.ToString();

        // OrderId Equals
        public override bool Equals(object? obj) => obj is OrderId other && Value.Equals(other.Value);

        // OrderId GetHashCode
        public override int GetHashCode() => Value.GetHashCode();

        public override IEnumerable<object> GetEqualityComponents()
        {
            throw new NotImplementedException();
        }

        // OrderId == operator
        public static bool operator ==(OrderId a, OrderId b) => a.Value == b.Value;

        // OrderId != operator
        public static bool operator !=(OrderId a, OrderId b) => !(a == b);
    }
}