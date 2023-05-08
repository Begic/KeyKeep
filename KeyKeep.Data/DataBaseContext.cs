using KeyKeep.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace KeyKeep.Data;

public class DataBaseContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Password> Passwords { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite(@"DataSource=MyDataBase.db;");
    }
}
