using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRentalSystem.Models;
using CarRentalSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace CarRentalSystem.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly CarModelDbContext _context;
        public TransactionRepository(CarModelDbContext context)
        {
            _context = context;
        }

        public async Task AddTransaction(TransactionModel transaction)
        {
            await _context.TransactionsList.AddAsync(transaction);
            await _context.SaveChangesAsync();
        }

        //to find user booking history
        public async Task<List<TransactionModel>> GetTransactionHistory(int userId)
        {
            return await _context.TransactionsList.Where(t => t.UserId == userId).ToListAsync();

        }
    }
}
