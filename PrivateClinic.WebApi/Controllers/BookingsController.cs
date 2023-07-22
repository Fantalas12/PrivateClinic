using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PrivateClinic.Persistence;
using PrivateClinic.Persistence.Services;
using PrivateClinic.DTO;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace PrivateClinic.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IPrivateClinicService _service;

        public BookingsController(IPrivateClinicService service)
        {
            _service = service;
        }

        
        // GET: api/Bookings
        [HttpGet]
        //public ActionResult<IEnumerable<Booking>> GetBookings()
        public ActionResult<IEnumerable<BookingDTO>> GetBookings()
        {
            /*
            return _service
                .GetBookings()
                .Select(booking => (BookingDTO)booking)
                .ToList();
            */

            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return _service
                .GetBookingsByDoctorId(id)
                .Select(booking => (BookingDTO)booking)
                .ToList();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult DeleteBooking(Int32 id)
        {
            if (_service.DeleteBookingById(id))
            {
                return Ok();
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }



        /*
        // GET: api/BookingsToDoctor/{id}
        [HttpGet]
        //public ActionResult<IEnumerable<Booking>> GetBookings()
        public ActionResult<IEnumerable<BookingDTO>> GetBookingsToDoctor()
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return _service
                .GetBookingsByDoctorId(id)
                .Select(booking => (BookingDTO)booking)
                .ToList();
        }
        */



        /*
        // GET: api/Booking/5
        [HttpGet("{id}")]
        public ActionResult<BookingDTO> GetBooking(Int32 id)
        {
            try
            {
                BookingDTO booking = (BookingDTO)_service.GetBookingById(id);
                return booking;
            }
            catch (InvalidOperationException)
            {

                return NotFound();
            }
        }
        */



    }
}
