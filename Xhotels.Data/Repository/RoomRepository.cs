using Microsoft.EntityFrameworkCore;
using Xhotels.Data.Models;

namespace Xhotels.Data.Repository
{
    public class RoomRepository : IRoomRepository
    {
        private readonly HotelContext _context;
        public RoomRepository(HotelContext context)
        {
            _context = context;
        }
        public async Task<Room?> GetRoomById(int roomId)
        {
            var room = await _context.Rooms.FirstOrDefaultAsync(r => r.Id == roomId);
            return room;
        }

        public async Task<decimal> GetRoomPricePerNight(int roomId)
        {
            var room = await GetRoomById(roomId);
            if (room == null)
            {
                throw new Exception("Invalid room");
            }
            return room.PricePerNight;
        }
    }
}
