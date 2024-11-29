using CarRentalSystem.Data;
using CarRentalSystem.Models;
using CarRentalSystem.Repositories;
namespace CarRentalSystem.Services
{
    public class CarService: ICarService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly ICarRepository _carRepository;
        private readonly CarModelDbContext _carModelDbContext;
        private readonly SmsService _smsService;

        public CarService(ICarRepository carRepository, CarModelDbContext carModelDbContext, ITransactionRepository transactionRepository, IUserRepository userRepository, SmsService smsService)
        {
            _transactionRepository = transactionRepository;
            _carModelDbContext = carModelDbContext;
            _carRepository = carRepository;
            _userRepository = userRepository;
            _smsService = smsService;
        }
        public async Task<String> RentCar(int userId, int carId, int days)
        {
            using (var transaction = await _carModelDbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var car = await _carRepository.GetCarById(carId);
           
                    if (car == null)
                    {
                        throw new Exception("car not Found");
                    }

                    if (!car.isAvailable)
                    {
                        throw new Exception("Car not Available.");
                    }

                    //calculating price
                    decimal totalPrice = await CalculateRentalPrice(carId, days);
                    car.isAvailable = false;
                    await _carRepository.UpdateCar(car);

                    //adding transaction entry
                    var transactionRecord = new TransactionModel
                    {
                        UserId = userId,
                        CarId = carId,
                        RentDays = days,
                        TotalAmount = totalPrice,
                        RentDate = DateTime.Now,
                    };
                    await _transactionRepository.AddTransaction(transactionRecord);
                    await transaction.CommitAsync();

                    //fetching user by id
                    var user = await _userRepository.GetUserById(userId);
                    if (user != null)
                    {
                        //sending sms
                        var suceed = await _smsService.SendSms(user.PhoneNumber, user.Name, car.Make, days, totalPrice);
                        if (!suceed)
                        {
                            throw new ArgumentException($"The phone number {user.PhoneNumber} is unverified");
                        }
                    }


                    return $"Car  has been rented by userId {userId} for {days} days. Total Price: ${totalPrice}";
                }
          
                
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new Exception($"{ex.Message}");
                }
            }
        }



        //calculate rentPrice
        public async Task<decimal> CalculateRentalPrice(int carId, int rentalDays)
        {
            var car = await _carRepository.GetCarById(carId);
            if (car == null)
            {
                return 0;
            }
            return car.PricePerDay * rentalDays;
        }

        public async Task<bool> IsCarAvailable(int CarId)
        {
            var car = await _carRepository.GetCarById(CarId);
            return car.isAvailable;
        }


        public async Task<List<TransactionModel>> GetBookingHistoryOfCar(int userId)
        {
            return await _transactionRepository.GetTransactionHistory(userId);
        }

        public async Task<IEnumerable<CarModel>> getAvailableCars()
        {
            return await _carRepository.GetAvailableCars();

        }

        //Get car by id
        public async Task<CarModel> getCarById(int id)
        {
            return await _carRepository.GetCarById(id);
        }

        //add car
        public async Task<CarModel> AddCar(CarModel car)
        {
            return await _carRepository.AddCar(car);
        }

        //update car details
        public async Task<CarModel> UpdateCar(CarModel  car)
        {
            return await _carRepository.UpdateCar(car);
        }

        //delete car details
        public async Task<bool> DeleteCar(int id)
        {
            return await _carRepository.DeleteCar(id);
        }
    }
}
