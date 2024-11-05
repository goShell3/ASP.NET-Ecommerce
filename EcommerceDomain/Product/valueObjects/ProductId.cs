using Ecommrce.Domain.Common;

namespace Ecoimmerce.Domian.Product.ValueObjects;

public sealed class ProductId : ValueObject {

    private ProductId(Guid value) {
        if (value == Guid.Empty) {
            throw new ArgumentException("Product id cannot be empty", nameof(value));
        }
        Value = value;
    }

    public Guid Value { get; }

    public static implicit operator Guid(ProductId self) => self.Value;

    public static implicit operator ProductId(Guid value) => new ProductId(value);

    public override string ToString() => Value.ToString();

    public override bool Equals(object? obj) => obj is ProductId other && Value.Equals(other.Value);

    public override int GetHashCode() => Value.GetHashCode();

    public override IEnumerable<object> GetEqualityComponents()
    {
        throw new NotImplementedException();
    }

    public static bool operator ==(ProductId a, ProductId b) => a.Value == b.Value;

    public static bool operator !=(ProductId a, ProductId b) => !(a == b);


}