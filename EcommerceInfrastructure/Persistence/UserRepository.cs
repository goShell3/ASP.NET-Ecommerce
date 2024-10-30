using System.Reflection.Metadata;
using Ecommerce.Application.Common.Interfaces.Persistence;

namespace Ecommerce.Infrastructure;

public class userRepository : IUserRepository{

    private readonly List<User> _users = new();

    public void AddUser(User user)
    {
        _users.Add(user);
    }

    public User? GetByUserEmail (int id) {
        throw new NotImplementedException();
    }

    public User? GetUserByEmail(string email)
    {
        throw new NotImplementedException();
    }
}