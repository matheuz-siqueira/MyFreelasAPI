using Microsoft.EntityFrameworkCore;
using MyFreelas.Domain.Entities;

namespace MyFreelas.Infra.Data.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<User> Users { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Freela> Freelas { get; set; }
    public DbSet<Installment> Installments { get; set; }   
}
