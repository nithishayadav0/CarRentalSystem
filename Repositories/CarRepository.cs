using CarRentalSystem.Data;
using CarRentalSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace CarRentalSystem.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly CarModelDbContext _context;
        public CarRepository(CarModelDbContext context)
        {
            _context = context;
        }

        //To get all the available cars
        public async Task<IEnumerable<CarModel>> GetAvailableCars()
        {

            return await _context.CarsList.Where(c => c.isAvailable).ToListAsync();
        }

        //to get each  car by id
        public async Task<CarModel> GetCarById(int id)
        {
            return await _context.CarsList.FindAsync(id);
        }

        //Add new car
        public async Task<CarModel> AddCar(CarModel car)
        {
            _context.CarsList.Add(car);
            await _context.SaveChangesAsync();
            return car;

        }

        //Update car details
        public async Task<CarModel> UpdateCar(CarModel updatedCar)
        {
            if (updatedCar == null)
                throw new ArgumentNullException(nameof(updatedCar), "Updated car cannot be null.");

            var existingCar = await _context.CarsList.FirstOrDefaultAsync(c => c.Id == updatedCar.Id);
            if (existingCar == null)
                throw new KeyNotFoundException($"Car with Id {updatedCar.Id} not found.");

            try
            {
                existingCar.Make = updatedCar.Make;
                existingCar.Model = updatedCar.Model;
                existingCar.Year = updatedCar.Year;
                existingCar.PricePerDay = updatedCar.PricePerDay;
                existingCar.isAvailable = updatedCar.isAvailable;

                await _context.SaveChangesAsync();
                return updatedCar;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while updating the car.", ex);
            }
        }


        //Delete a car
        public async Task<bool> DeleteCar(int id)
        {
            var car = await _context.CarsList.FindAsync(id);
            if (car == null)
            {
                return false;

            }
            _context.CarsList.Remove(car);
            await _context.SaveChangesAsync();
            return true;
        }


    }
}
