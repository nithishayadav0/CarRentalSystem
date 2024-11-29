using CarRentalSystem.Models;

namespace CarRentalSystem.Repositories
{
    public interface ICarRepository
    {
        public Task<IEnumerable<CarModel>> GetAvailableCars();
        public Task<CarModel> GetCarById(int id);
        public Task<CarModel> AddCar(CarModel car);
        public Task<CarModel> UpdateCar(CarModel car);

        public Task<bool> DeleteCar(int id);

    }
}
