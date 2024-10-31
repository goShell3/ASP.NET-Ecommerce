namespace Ecommerce.Application.Common.Interfaces;

public interface IDateTimeProvider{
    DateTime Utcnow { get; }
}