using Ecommrce.Domain.Common;

namespace Ecoimmerce.Domian.Product.ValueObjects;

public sealed class UserId : ValueObject {

    private UserId(Guid value) {
        if (value == Guid.Empty) {
            throw new ArgumentException("Product id cannot be empty", nameof(value));
        }
        Value = value;
    }

    public Guid Value { get; }

    public static implicit operator Guid(UserId self) => self.Value;

    public static implicit operator UserId(Guid value) => new UserId(value);

    public override string ToString() => Value.ToString();

    public override bool Equals(object? obj) => obj is UserId other && Value.Equals(other.Value);

    public override int GetHashCode() => Value.GetHashCode();

    public override IEnumerable<object> GetEqualityComponents()
    {
        throw new NotImplementedException();
    }

    public static bool operator ==(UserId a, UserId b) => a.Value == b.Value;

    public static bool operator !=(UserId a, UserId b) => !(a == b);


}