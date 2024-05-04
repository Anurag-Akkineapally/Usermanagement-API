using Microsoft.EntityFrameworkCore;

namespace UsersApi_Real.Data;
public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
}