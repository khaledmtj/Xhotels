namespace Xhotels.Data.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public DateTime ReservationDate { get; set; }
        public int RoomId { get; set; }
        public int CustomerId { get; set; }
        public int NumberOfNights { get; set; }
        public decimal TotalPrice { get; set; }

        public Room Room { get; set; }
        public Customer Customer { get; set; }
    }
}
