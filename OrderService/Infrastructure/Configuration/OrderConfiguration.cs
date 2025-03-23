using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.Shared.Models;

namespace OrderService.Infrastructure.Configuration;

public class OrderConfiguration: IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders"); // Table name

        builder.HasKey(o => o.Id); // Primary Key
        builder.HasIndex(o => o.OrderReference).IsUnique();
        builder.HasIndex(o => o.MobileNumber).IsUnique();

        builder.Property(o => o.Id)
            .ValueGeneratedOnAdd(); // Auto-increment

        builder.Property(o => o.CustomerName)
            .IsRequired()
            .HasMaxLength(100); // Ensures it's required and limits length

        builder.Property(o => o.TotalAmount)
            .IsRequired()
            .HasColumnType("decimal(10,2)"); // Correct precision for money
        
        builder.Property(o => o.MobileNumber)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(o => o.OrderDate)
            .HasDefaultValueSql("CURRENT_TIMESTAMP"); 
    }
}