using Ecommerce.Application.Common.Interfaces;

namespace Ecommerce.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider {

    public DateTime Utcnow => DateTime.UtcNow;
}