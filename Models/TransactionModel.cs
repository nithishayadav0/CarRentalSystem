namespace CarRentalSystem.Models
{
    public class TransactionModel
    {
        public int Id { get; set; }
        public  int UserId { get; set; }
        public int CarId { get; set; }
        public int RentDays { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime RentDate { get; set; }


        public UserModel User { get; set; }
        public CarModel Car { get; set; }
    }
}
