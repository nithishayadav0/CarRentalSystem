Car Rental System API using C#

The Car Rental System API provides functionality to manage a fleet of cars, including booking,
availability checks, rental history, and user management. The system will be built using C#,
Entity Framework (EF). Below are the tasks to be completed for each part of the system.

1. Models
   • Car Model: Represents a car in the rental system.
   
       o Properties: Id, Make, Model, Year, PricePerDay, IsAvailable (boolean
        indicating availability)
   

• User Model: Represents a user who can rent cars.

        o Properties: Id, Name, Email, Password, Role (Admin or User)
    
2. Services
   
    • Car Rental Service: Handles the core business logic for renting cars, including
    checking availability and calculating rental prices.
   
       o Methods: RentCar, CheckCarAvailability
   
    • User Service: Handles user management, including user registration and
    authentication.
   
        o Methods: RegisterUser, AuthenticateUser (returns JWT token)
4. Repositories
   
    • Car Repository: Manages data operations for the Car model.

        o Methods: AddCar, GetCarById, GetAvailableCars, UpdateCarAvailability
    • User Repository: Manages data operations for the User model.
   
        o Methods: AddUser, GetUserByEmail, GetUserById

6. Controllers for CRUD Operations
   
    • Car Controller: Exposes API endpoints for managing cars.
   
    o Endpoints:
   
        ▪ GET /cars: Get a list of available cars
        ▪ POST /cars: Add a new car to the CarList
        ▪ PUT /cars/: Update car details and availability
        ▪ DELETE/cars/{id}: Delete the car details

    • User Controller: Exposes API endpoints for user registration and authentication.
   
    o Endpoints:
   
        ▪ POST /users/register: Register a new user
        ▪ POST /users/login: Login and get JWT token
8. Notification Handling System:
   
    Implement a simple SMS notification system to notify users when their car
    booking is successful using TWILIO.
   
6.Finally ,Test the API endpoints using Postman.
For more INFORMATION,Take a look at here [View the PDF file](CaRenaltSS.pdf)
)
