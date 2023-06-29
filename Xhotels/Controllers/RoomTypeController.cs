using Microsoft.AspNetCore.Mvc;
using Xhotels.Data.Models;
using Xhotels.Data.Repository;

namespace Xhotels.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomTypeController : ControllerBase
    {
        private readonly IRoomTypeRepository _roomTypeRepository;
        public RoomTypeController(IRoomTypeRepository roomTypeRepository)
        {
            _roomTypeRepository = roomTypeRepository;
        }
        [HttpGet]
        public async Task<List<RoomType>> GetAllRoomTypes()
        {
            var roomTypes = await _roomTypeRepository.GetAllRoomTypes();
            return roomTypes;
        }
    }
}
