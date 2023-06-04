using HairSalonWEB.Models;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql;

public class DataContext : DbContext
{
    string MySqlConnectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=hair_salon;Convert Zero Datetime=True";
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }
    public DbSet<administrator> Administrator { get; set; }
    public DbSet<company> Company { get; set; }
    public DbSet<client> Client { get; set; }
    public DbSet<master> Master { get; set; }
    public DbSet<procedures> Procedures { get; set; }
    public DbSet<recordd> Record { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string serverVersion = "10.4.25-MariaDB";
        optionsBuilder.UseMySql(MySqlConnectionString, new MySqlServerVersion(serverVersion));
    }
}
