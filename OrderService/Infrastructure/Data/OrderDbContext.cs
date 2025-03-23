using System.Reflection;
using Microsoft.EntityFrameworkCore;
using OrderService.Shared.Models;

namespace OrderService.Shared.Data;

public class OrderDbContext(DbContextOptions<OrderDbContext> options) : DbContext(options)
{
    public DbSet<Order> Orders { get; set; } 
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Automatically apply all configurations in the assembly
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}