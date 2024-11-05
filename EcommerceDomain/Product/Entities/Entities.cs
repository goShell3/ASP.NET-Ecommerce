namespace Ecommerce.Domain.Models;

// define the common entities 
public abstract class BaseEntity<TId>:IEquatable<BaseEntity<TId>> where TId : notnull {
    public TId EntityId { get; protected set; }

    protected BaseEntity(TId entityId) {
        EntityId = entityId;
    }

    public bool Equals (BaseEntity<TId>? entity) {
        return Equals((object?) entity);
    }

    public override string ToString() {
        return $"Entity of type {GetType().Name} with Id: {EntityId}";
    }

    public override bool Equals(object? obj) {
        return obj is BaseEntity<TId> other && EntityId.Equals(other.EntityId);
    }

    public static bool operator ==(BaseEntity<TId> a, BaseEntity<TId> b) {
        return a.Equals(b);
    }
    public static bool operator!=(BaseEntity<TId> a, BaseEntity<TId> b) {
        return !(a == b);
    }

    public override int GetHashCode() {
            return EntityId.GetHashCode();
        }
}
