using Ecommerce.Domain.Models;  // Assuming this is where the User model is defined
using Ecommerce.Application.Common.Interfaces.Persistence; // Assuming this is where IUserRepository is defined
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure
{
    public class UserRepository : IUserRepository
    {
        // Static list to simulate a database for users
        private static readonly List<User> _users = new();

        // Add a user to the list
        public void AddUser(User user)
        {
            _users.Add(user);
        }

        // Retrieve a user by ID
        public Task<User?> GetByIdAsync(Guid userId)
        {
            var user = _users.SingleOrDefault(u => u.Id == userId);
            return Task.FromResult(user);
        }

        public Task<User> GetByIdAsync(object userId)
        {
            // if (userId is Guid guid)
            // {
            //     var user = _users.SingleOrDefault(u => u.Id == guid);
            //     return Task.FromResult(user);
            // }
            // return Task.FromResult<User>(null);
            throw new NotImplementedException();
        }

        // Retrieve a user by email
        public User? GetUserByEmail(string email)
        {
            return _users.SingleOrDefault(u => u.Email == email);
        }

    
    }
}
