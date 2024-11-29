using CarRentalSystem.Data;
using CarRentalSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentalSystem.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly CarModelDbContext _context;

        public UserRepository(CarModelDbContext context)
        {
            _context = context;
        }

        public async Task AddUser(UserModel user)
        {
            _context.UsersList.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<UserModel> GetUserByEmail(string email)
        {
            return await _context.UsersList.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<UserModel> GetUserById(int id)
        {
            return await _context.UsersList.FindAsync(id);
        }
    }
}
