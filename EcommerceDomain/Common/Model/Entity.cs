namespace Ecommerce.Domain.Models;

public abstract class Entity<TId>:IEquatable<Entity<TId>> where TId : notnull {
    public TId EntityId { get; protected set; }

    protected Entity(TId entityId) {
        EntityId = entityId;
    }

    public bool Equals (Entity<TId>? entity) {
        return Equals((object?) entity);
    }

    public override string ToString() {
        return $"Entity of type {GetType().Name} with Id: {EntityId}";
    }

    public override bool Equals(object? obj) {
        return obj is Entity<TId> other && EntityId.Equals(other.EntityId);
    }

    public static bool operator ==(Entity<TId> a, Entity<TId> b) {
        return a.Equals(b);
    }
    public static bool operator!=(Entity<TId> a, Entity<TId> b) {
        return !(a == b);
    }

    public override int GetHashCode() {
        return EntityId.GetHashCode();  
    }

}