using CarRentalSystem.Models;

namespace CarRentalSystem.Repositories
{
    public interface IUserRepository
    {
        public Task AddUser(UserModel user);
        public Task<UserModel> GetUserByEmail(string email);
        public Task<UserModel> GetUserById(int id);
    }
}
