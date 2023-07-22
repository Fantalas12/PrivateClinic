using Microsoft.AspNetCore.Mvc;
using PrivateClinic.Persistence;
using PrivateClinic.Persistence.Services;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;

namespace PrivateClinic.Web.Controllers
{

    //[Authorize]
    public class HomeController : Controller
    {

        private readonly IPrivateClinicService _service;

        public HomeController(IPrivateClinicService service)
        {
            _service = service;
        }

        // GET: Doctors
        public IActionResult Index()
        {
            //Orvosok listája névsor szerint rendezve
            List<Doctor> doctors = _service.GetDoctors().OrderBy(i => i.Name).ToList();
            return View(doctors);
        }

        public IActionResult? DisplayImage(string id)
        {
            var item = _service.GetDoctorById(id);
            if (item != null && item.Image != null)
            {
                return File(item.Image, "image/png");
            }
            return null;
        }


    }
   
}