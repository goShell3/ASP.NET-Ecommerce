
namespace Ecommerce.Application.Common.Interfaces.Persistence;

public interface IUserRepository {
    User? GetUserByEmail(string email);
    void AddUser(User user);
    public Task<User> GetByIdAsync(object userId);
}