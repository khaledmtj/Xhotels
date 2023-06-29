using Xhotels.Data.Models;

namespace Xhotels.Data.Repository
{
    public interface IRoomTypeRepository
    {
        public Task<List<RoomType>> GetAllRoomTypes();
        public Task<RoomType?> GetRoomTypeById(int roomId);
    }
}
