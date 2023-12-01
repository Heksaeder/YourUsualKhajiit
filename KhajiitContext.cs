namespace Khajiit
{

  using Microsoft.EntityFrameworkCore;
  // using Pomelo.EntityFrameworkCore.MySql;

  public class KhajiitContext : DbContext
  {
    public DbSet<Item> Items { get; set; }
    public DbSet<Weapon> Weapons { get; set; }
    public DbSet<Armor> Armors { get; set; }
    public DbSet<Property> Properties { get; set; }
    public DbSet<Item_Properties> Item_Properties { get; set; }
    public DbSet<Warehouse> Warehouse { get; set; }
    public DbSet<Vendor> Vendors { get; set; }
    public DbSet<Vendor_Inventory> Vendor_Inventory { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      if (!optionsBuilder.IsConfigured)
      {
        // To put in a config file/env var later
        string connectionString = "Server=localhost;Database=khajiit;User=root;Password=;";

        var servVersion = new MySqlServerVersion(new Version(8, 0, 26));
        optionsBuilder.UseMySql(connectionString, servVersion);
      }
    }
  }
}