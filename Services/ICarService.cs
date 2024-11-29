using CarRentalSystem.Models;

namespace CarRentalSystem.Services
{
    public interface ICarService
    {
        public Task<string> RentCar(int userId, int carId, int days);
        public Task<bool> IsCarAvailable(int CarId);
        public Task<decimal> CalculateRentalPrice(int carId, int rentalDays);
        public Task<List<TransactionModel>> GetBookingHistoryOfCar(int userId);
        public Task<IEnumerable<CarModel>> getAvailableCars();
        public Task<CarModel> getCarById(int id);
        public Task<CarModel> AddCar(CarModel car);
        public Task<CarModel> UpdateCar(CarModel car);
        public Task<bool> DeleteCar(int id);
    }
}
