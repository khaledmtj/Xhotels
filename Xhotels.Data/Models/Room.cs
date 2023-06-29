namespace Xhotels.Data.Models
{
    public class Room
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int RoomTypeId { get; set; }
        public decimal PricePerNight { get; set; }
        public int Floor { get; set; }
        public int NumberOfBeds { get; set; }

        public RoomType RoomType { get; set; }
    }
}
