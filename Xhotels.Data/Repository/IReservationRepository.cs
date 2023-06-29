using Xhotels.Data.Models;

namespace Xhotels.Data.Repository
{
    public interface IReservationRepository
    {
        public Task<List<Reservation>> GetAllReservations();
        public Task<Reservation?> GetReservationById(int reservationId);
        public Task<Reservation> InsertReservation(Reservation reservation);
        public Task<bool> IsConflictingReservations(int roomId, DateTime reservationDate, int numberOfNights);
        public Task<int> GetAvailableRoomId(int roomTypeId, DateTime reservationDate, int numberOfNights);
        public Task<List<int>> GetReservedRoomIds(int roomTypeId, DateTime reservationDate, int numberOfNights);
    }
}
