using CarRentalSystem.Models;
using CarRentalSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : Controller
    {
        private readonly ICarService _carService;
        public CarController(ICarService carService)
        {
            this._carService = carService;
        }
        [HttpPost("rent")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> RentCar(RentDTO rent)
        {
            int userId = GetUserIdFromClaims();
            try
            {
                var result = await _carService.RentCar(userId, rent.CarId, rent.Days);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //getting the user booking history
        [HttpGet("BookingHistory")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetBookingHistory()
        {
            int userId = GetUserIdFromClaims();
            var history = await _carService.GetBookingHistoryOfCar(userId);
            if (history == null || !history.Any())
            {
                return NotFound("No booking history found");
            }
            return Ok(history);
        }
        [HttpGet("GetAvailableCars")]
        public async Task<IActionResult> GetAvailableCars()
        {
            var cars = await _carService.getAvailableCars();
            return Ok(cars);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCarById(int id)
        {
            var car = await _carService.getCarById(id);
            if (car == null)
            {
                return NotFound($"Car with ID {id} is not found.");
            }
            return Ok(car);
        }
        //post request to add cars by admin
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddCar(CarModel car)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var addedCar = await _carService.AddCar(car);
            return CreatedAtAction(nameof(AddCar), new { id = car.Id }, car);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCar(CarModel car)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var updatedcar = await _carService.UpdateCar(car);
                return Ok(car);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

       
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            var success = await _carService.DeleteCar(id);
            if (!success)
            {
                return NotFound($"Car with ID {id} is not found.");
            }
            return NoContent();



        }
        private  int GetUserIdFromClaims()
        {
            var userClaims = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "userId");
            if (userClaims == null)
            {
                throw new UnauthorizedAccessException("Invalid User");
            }
            return int.Parse(userClaims.Value);
        }


    }
}
