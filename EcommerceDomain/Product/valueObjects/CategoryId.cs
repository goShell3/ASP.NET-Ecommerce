using Ecommrce.Domain.Common;

public sealed class CategoryId : ValueObject {

    private CategoryId(Guid value) {
        if (value == Guid.Empty) {
            throw new ArgumentException("Category id cannot be empty", nameof(value));
        }
        Value = value;
    }

    public Guid Value { get; }

    public static implicit operator Guid(CategoryId self) => self.Value;

    public static implicit operator CategoryId(Guid value) => new CategoryId(value);

    public override string ToString() => Value.ToString();

    public override bool Equals(object? obj) => obj is CategoryId other && Value.Equals(other.Value);

    public override int GetHashCode() => Value.GetHashCode();

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static bool operator ==(CategoryId a, CategoryId b) => a.Value == b.Value;
    public static bool operator !=(CategoryId a, CategoryId b) => !(a == b);
}