using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PrivateClinic.Persistence;
using PrivateClinic.Persistence.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;


namespace PrivateClinic.Web.Controllers
{

    public class CalendarController : Controller
    {
        private readonly IPrivateClinicService _service;

        public CalendarController(IPrivateClinicService service)
        {
            _service = service;
        }

        //ELőállítja az orvoshoz tartozó foglalási naptárat...
        public IActionResult Index(string? doctorId, int? specializationId, int pageid)
        {


            var viewModel = new CalendarViewModel
            {
                Date = DateTime.Now,
                Bookings = _service.GetBookings(doctorId, specializationId),
                Specializations = _service.GetSpecializations(),
                SelectedDoctor = _service.GetDoctorById(doctorId),
                SelectedSpecializationId = specializationId ?? 0,
                booking = new Booking(),
                pageid = pageid

            };

            viewModel.booking.PatientID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            viewModel.booking.Patient = _service.GetPatientById(viewModel.booking.PatientID);

            return View(viewModel);
        }


        //Feldolgozzuk a foglalásokat, a GET metódusnak ami meghívja a feldolgozást, az az index
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Book(Booking booking, string doctorid, CalendarViewModel cwm)
        {
            booking.DoctorID = doctorid;
            booking.Doctor = _service.GetDoctorById(booking.DoctorID);
            booking.PatientID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            booking.Patient = _service.GetPatientById(booking.PatientID);
            booking.Comment = cwm.Comment;
            if(cwm.SelectedSpecializationId != 0) booking.SpecializationId = cwm.SelectedSpecializationId;  




            if (!ModelState.IsValid)
            {

                try
                {
                    _service.CreateBooking(booking);
                    TempData["SuccessMessage"] = "Sikeres foglalás";
                }
                catch (InvalidOperationException ex)
                {
                    TempData["ErrorMessage"] = ex.Message;
                }
                catch
                {
                    TempData["ErrorMessage"] = "Hibatörtént a foglalás során";
                }

                return RedirectToAction("Index", "Home");

            }
            return View(booking);
        }



        // GET
        public IActionResult BookWithSpecialization(int specializationId)
        {

            Booking booking = new Booking();
            booking.SpecializationId = specializationId;
            Doctor doctor = _service.LeastBookedDoctor(booking);
            return RedirectToAction("Index", "Calendar", new { doctorId = doctor.Id,  specializationId = specializationId});
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Booking booking = new Booking();
            try
            {
                booking = _service.GetBooking(id);

            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }

            if (booking.PatientID == User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                _service.DeleteBookingById(id);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return Forbid();
            }
        }


    
    }
}