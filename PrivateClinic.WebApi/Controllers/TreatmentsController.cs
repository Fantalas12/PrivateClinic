using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PrivateClinic.Persistence;
using PrivateClinic.Persistence.Services;
using PrivateClinic.DTO;
using Microsoft.AspNetCore.Authorization;

namespace PrivateClinic.WebApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TreatmentsController : ControllerBase
    {
        private readonly IPrivateClinicService _service;

        public TreatmentsController(IPrivateClinicService service)
        {
            _service = service;
        }

        // GET: api/Treatments
        [HttpGet]
        public ActionResult<IEnumerable<TreatmentDTO>> GetTreatments(Int32 medrecordid)
        {
            
            return _service
                .GetTreatments().
                Where(i=> i.MedRecordId == medrecordid)
                .Select(treatment => (TreatmentDTO)treatment)
                .ToList();
           
            /*
            return _service
                .GetMedicalRecordById(medrecordid)
                .Treatments
                .Select(treatment => (TreatmentDTO)treatment)
                .ToList();
            */
        }


        // GET: api/Treatments/5
        [HttpGet("{id}")]
        public ActionResult<TreatmentDTO> GetTreatment(Int32 id)
        {
            try
            {
                var treatment = _service.GetTreatmentById(id);
                return (TreatmentDTO)treatment;
            }
            catch (InvalidOperationException)
            {

                return NotFound();
            }
        }

        // PUT: api/Treatments/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult PutTreatment(Int32 id, TreatmentDTO treatmentdto)
        {
            if (id != treatmentdto.Id)
            {
                return BadRequest();
            }

            var treatment = (Treatment)treatmentdto;

            if (_service.UpdateTreatment(treatment))
            {
                return Ok();
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        // POST: api/Items
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [Authorize]
        [HttpPost]
        public ActionResult<TreatmentDTO> PostTreatment(TreatmentDTO treatmentdto)
        {
            var MedicalRecord = _service.GetMedicalRecordById(treatmentdto.MedRecordId);
            var treatment = (Treatment)treatmentdto;
            treatment.MedicalRecord = MedicalRecord;

            var newtreatment = _service.CreateTreatment(treatment);

            if (newtreatment is null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            else
            {
                return CreatedAtAction(nameof(GetTreatment), new { id = treatment.Id },
                    (TreatmentDTO)treatment);
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult DeleteTreatment(int id)
        {
            if (_service.DeleteTreatment(id))
            {
                return Ok();
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }



    }
}
