using Ecommrce.Domain.Common;

public sealed class StockId : ValueObject {

    private StockId(Guid value) {
        if (value == Guid.Empty) {
            throw new ArgumentException("Category id cannot be empty", nameof(value));
        }
        Value = value;
    }

    public Guid Value { get; }

    public static implicit operator Guid(StockId self) => self.Value;

    public static implicit operator StockId(Guid value) => new StockId(value);

    public override string ToString() => Value.ToString();

    public override bool Equals(object? obj) => obj is StockId other && Value.Equals(other.Value);

    public override int GetHashCode() => Value.GetHashCode();

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static bool operator ==(StockId a, StockId b) => a.Value == b.Value;
    public static bool operator !=(StockId a, StockId b) => !(a == b);
}