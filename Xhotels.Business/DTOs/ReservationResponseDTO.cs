using Xhotels.Data.Models;

namespace Xhotels.Business.DTOs
{
    public class ReservationResponseDTO
    {
        public DateTime ReservationDate { get; set; }
        public int NumberOfNights { get; set; }
        public decimal TotalPrice { get; set; }

        public Room Room { get; set; }
        public Customer Customer { get; set; }
    }
}
