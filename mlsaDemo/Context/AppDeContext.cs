using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using mlsaDemo.Models;

namespace mlsaDemo.Context
{
   public  class AppDbContext : DbContext
   {

       private readonly DbSettings _dbSettings;
       public AppDbContext(IOptions<DbSettings> dbSettings)
       {
           _dbSettings = dbSettings.Value;
       }

       public DbSet<ItemsModel> Items { get; set; }


       protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
       {
           optionsBuilder.UseSqlServer(_dbSettings.ConnectionString);
       }


       protected override void OnModelCreating(ModelBuilder modelBuilder)
       {
           modelBuilder.Entity<ItemsModel>()
               .ToTable("items")
               .HasKey(x => x.Id);
       }
    }
}
