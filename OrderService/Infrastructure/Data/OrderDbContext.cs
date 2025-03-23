using Microsoft.EntityFrameworkCore;
using OrderService.Shared.Models;

namespace OrderService.Infrastructure.Data;

public class OrderDbContext : DbContext
{
    public OrderDbContext()
    {
        
    }
    
    public OrderDbContext(DbContextOptions<OrderDbContext> options)
        : base(options)
    {
    }
    
    public virtual DbSet<Order> Orders { get; set; }
}