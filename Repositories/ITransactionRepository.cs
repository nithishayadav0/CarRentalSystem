using CarRentalSystem.Models;

namespace CarRentalSystem.Repositories
{
    public interface ITransactionRepository
    {
        public Task AddTransaction(TransactionModel transaction);
        public Task<List<TransactionModel>> GetTransactionHistory(int userId);
    }
}
