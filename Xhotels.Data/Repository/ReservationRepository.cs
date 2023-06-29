using Microsoft.EntityFrameworkCore;
using Xhotels.Data.Models;

namespace Xhotels.Data.Repository
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly HotelContext _context;

        public ReservationRepository(HotelContext hotelContext)
        {
            _context = hotelContext;
        }
        public async Task<Reservation> InsertReservation(Reservation reservation)
        {
            var addedReservation = await _context.Reservations.AddAsync(reservation);
            await _context.SaveChangesAsync();

            return addedReservation.Entity;
        }

        public async Task<List<Reservation>> GetAllReservations()
        {
            List<Reservation> reservations = await _context.Reservations.ToListAsync();
            return reservations;
        }

        public Task<Reservation?> GetReservationById(int reservationId)
        {
            var reservation = _context.Reservations.FirstOrDefaultAsync(r => r.Id == reservationId);
            return reservation;
        }

        public async Task<bool> IsConflictingReservations(int roomId, DateTime reservationDate, int numberOfNights)
        {
            bool isConflicting = await _context.Reservations.AnyAsync(r =>
                r.RoomId == roomId &&
                r.ReservationDate.AddDays(r.NumberOfNights - 1) >= reservationDate &&
                r.ReservationDate <= reservationDate.AddDays(numberOfNights - 1)
            );

            return isConflicting;
        }

        public async Task<int> GetAvailableRoomId(int roomTypeId, DateTime reservationDate, int numberOfNights)
        {
            List<int> reservedRoomIds = await GetReservedRoomIds(roomTypeId, reservationDate, numberOfNights);

            int availableRoomId = await _context.Rooms
                .Where(r => r.RoomTypeId == roomTypeId && !reservedRoomIds.Contains(r.Id))
                .Select(r => r.Id)
                .FirstOrDefaultAsync();

            return availableRoomId;
        }

        public async Task<List<int>> GetReservedRoomIds(int roomTypeId, DateTime reservationDate, int numberOfNights)
        {

            var reservedRoomIds = await _context.Reservations
                .Join(
                    _context.Rooms,
                    reservation => reservation.RoomId,
                    room => room.Id,
                    (reservation, room) => new { Reservation = reservation, Room = room }
                )
                .Where(x => x.Room.RoomTypeId == roomTypeId &&
                            x.Reservation.ReservationDate.AddDays(x.Reservation.NumberOfNights - 1) >= reservationDate &&
                            x.Reservation.ReservationDate <= reservationDate.AddDays(numberOfNights - 1))
                .Select(x => x.Room.Id)
                .ToListAsync();

            return reservedRoomIds;
        }
    }
}
