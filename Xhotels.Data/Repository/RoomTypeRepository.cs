using Microsoft.EntityFrameworkCore;
using Xhotels.Data.Models;

namespace Xhotels.Data.Repository
{
    public class RoomTypeRepository : IRoomTypeRepository
    {
        private readonly HotelContext _context;
        public RoomTypeRepository(HotelContext context)
        {
            _context = context;
        }

        public async Task<List<RoomType>> GetAllRoomTypes()
        {
            var roomTypes = await _context.RoomTypes.ToListAsync();

            return roomTypes;
        }

        public async Task<RoomType?> GetRoomTypeById(int roomId)
        {
            var roomType = await _context.RoomTypes.FirstOrDefaultAsync(r => r.Id == roomId);
            return roomType;
        }
    }
}
