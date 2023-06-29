using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xhotels.Business.DTOs;
using Xhotels.Business.Services;
using Xhotels.Common.Exceptions;
using Xhotels.Data.Models;
using Xhotels.Data.Repository;

namespace Xhotels.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        private readonly IReservationRepository _reservationRepository;

        public ReservationController(IReservationService reservationService, IReservationRepository reservationRepository)
        {
            _reservationService = reservationService;
            _reservationRepository = reservationRepository;
        }

        [HttpGet]
        public async Task<List<Reservation>> GetReservations()
        {
            List<Reservation> reservations = await _reservationRepository.GetAllReservations();

            return reservations;
        }

        [HttpPost]
        public async Task<IActionResult> MakeReservation(ReservationDTO request)
        {
            try
            {
                var resp = await _reservationService.MakeReservation(request);
                return Ok(resp);
            }
            catch (CustomException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
