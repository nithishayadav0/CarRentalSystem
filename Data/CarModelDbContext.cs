using System.Transactions;
using CarRentalSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentalSystem.Data
{
    public class CarModelDbContext : DbContext
    {
        public CarModelDbContext(DbContextOptions<CarModelDbContext> options) : base(options) { }

        public DbSet<CarModel> CarsList { get; set; }
        public DbSet<UserModel> UsersList { get; set; }
        public DbSet<TransactionModel> TransactionsList { get; set; }
    }
}
