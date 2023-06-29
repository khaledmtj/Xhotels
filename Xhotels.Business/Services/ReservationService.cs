using Xhotels.Business.DTOs;
using Xhotels.Common.Exceptions;
using Xhotels.Data.Models;
using Xhotels.Data.Repository;

namespace Xhotels.Business.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IRoomRepository _roomRepository;
        public ReservationService(IReservationRepository reservationRepository, IRoomRepository roomRepository)
        {
            _reservationRepository = reservationRepository;
            _roomRepository = roomRepository;
        }
        public async Task<ReservationResponseDTO> MakeReservation(ReservationDTO reservation)
        {
            if (reservation.ReservationDate < DateTime.Today) {
                throw new CustomException("Reservation has to be in the future");
            }

            var availableRoomId = await _reservationRepository.GetAvailableRoomId(reservation.RoomTypeId, reservation.ReservationDate,
                reservation.NumberOfNights);

            if (availableRoomId == default(int))
            {
                throw new CustomException($"No available rooms found during the demanded time frame");
            }

            var pricePerNight = await _roomRepository.GetRoomPricePerNight(availableRoomId);
            var totalPrice = pricePerNight * reservation.NumberOfNights;
            Reservation newReservation = new Reservation
            {
                ReservationDate = reservation.ReservationDate,
                RoomId = availableRoomId,
                CustomerId = reservation.CustomerId,
                NumberOfNights = reservation.NumberOfNights,
                TotalPrice = totalPrice
            };
            Reservation addedReservation = await _reservationRepository.InsertReservation(newReservation);
            var reservationResponse = new ReservationResponseDTO
            {
                ReservationDate = addedReservation.ReservationDate,
                NumberOfNights = addedReservation.NumberOfNights,
                TotalPrice = addedReservation.TotalPrice,
                Room = addedReservation.Room,
                Customer = addedReservation.Customer
            };
            return reservationResponse;
        }
    }
}
