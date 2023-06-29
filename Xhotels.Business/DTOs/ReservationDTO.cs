namespace Xhotels.Business.DTOs
{
    public class ReservationDTO
    {
        public DateTime ReservationDate { get; set; }
        public int RoomTypeId { get; set; }
        public int CustomerId { get; set; }
        public int NumberOfNights { get; set; }
    }
}
