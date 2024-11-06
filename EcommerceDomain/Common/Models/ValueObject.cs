namespace Ecommrce.Domain.Common;

public abstract class ValueObject {
    public abstract IEnumerable<object> GetEqualityComponents();

    public override bool Equals(object? obj) {
        if (obj == null || obj.GetType() != GetType()) {
            return false;
        }
        var other = (ValueObject) obj;
        return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());

    }
    public static bool operator ==(ValueObject left, ValueObject right) {
        return Equals(left, right);
    }
    public static bool operator !=(ValueObject left, ValueObject right) {
        return !Equals(left, right);
    }
    public override int GetHashCode() {
        return GetEqualityComponents()
            .Select(x => x != null ? x.GetHashCode() : 0)
            .Aggregate((x, y) => x ^ y);
    }

    public bool Equals(ValueObject? other) {
        return Equals((object?) other);
    }
 }
// public class Price : ValueObject {
//     public decimal Value { get; }

//     public string Currency { get; }
    
//     public Price(decimal value, string currency) {
//         Value = value;
//         Currency = currency;
//     }
    
//     public override string ToString() => $"{Value} {Currency}";

//     public override IEnumerable<object> GetEqualityComponents()
//     {
//         yield return Value;
//         yield return Currency;
//     }
// }