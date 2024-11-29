using CarRentalSystem.Models;
using CarRentalSystem.Repositories;

namespace CarRentalSystem.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly JWTTOKENService _jwtTokenService;

        public UserService(IUserRepository userRepository, JWTTOKENService jwtTokenService)
        {
            _userRepository = userRepository;
            _jwtTokenService = jwtTokenService;
        }

        // Registers a new user
        public async Task<bool> RegisterUser(UserModel user)
        {
            // Check if the user already exists
            var existingUser = await _userRepository.GetUserByEmail(user.Email);
            if (existingUser != null)
            {
                throw new Exception("User already exists.");
            }

            // Hash the user's password before saving
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            // Add the user to the database
            await _userRepository.AddUser(user);
            return true;
        }

        // Authenticates a user and returns a JWT token
        public async Task<string> AuthenticateUser(string email, string password)
        {
            // Retrieve the user by email
            var user = await _userRepository.GetUserByEmail(email);

            // Check if user is null
            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid credentials.");
            }

            // Check if user role is null or empty
            if (string.IsNullOrEmpty(user.Role))
            {
                throw new InvalidOperationException("User role cannot be null or empty.");
            }

            // Check if user email is null or empty
            if (string.IsNullOrEmpty(user.Email))
            {
                throw new InvalidOperationException("User email cannot be null or empty.");
            }

            // Verify password
            if (!VerifyPassword(password, user.Password))
            {
                throw new UnauthorizedAccessException("Invalid credentials.");
            }

            // Generate a JWT token
            var token = _jwtTokenService.GenerateToken(user.Id, user.Name, user.Email, user.Role);
            return token;
        }


        // Verifies a plaintext password against a hashed password
        private bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
