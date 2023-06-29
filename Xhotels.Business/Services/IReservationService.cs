using Xhotels.Business.DTOs;

namespace Xhotels.Business.Services
{
    public interface IReservationService
    {
        public Task<ReservationResponseDTO> MakeReservation(ReservationDTO reservation);
    }
}
