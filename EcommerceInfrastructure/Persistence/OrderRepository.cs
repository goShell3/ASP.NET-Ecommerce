using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Domain.Models;
using Ecommerce.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Data
{
    public class OrderRepository : IOrderRepository
    {
        private readonly EcommerceDbContext _context;

        public OrderRepository(EcommerceDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order is not null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
            
        }

        public Task<IEnumerable<Order>> GetAllAsync()
        {
            return Task.FromResult(_context.Orders.AsEnumerable());
        }

        public async Task<Order> GetById(Guid id)
        {
            // return await _context.Orders
            //     .FirstOrDefaultAsync(o => o.Id == id);
            var order = await _context.Orders.FindAsync(id);
            if (order is null)
            {
                throw new KeyNotFoundException($"Order with id {id} not found.");
            }
            return order;
        }

        public async Task UpdateAsync(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }
    }
}