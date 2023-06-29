using Xhotels.Data.Models;

namespace Xhotels.Data.Repository
{
    public interface IRoomRepository
    {
        public Task<Room?> GetRoomById(int roomId);
        public Task<decimal> GetRoomPricePerNight(int roomId);
    }
}
