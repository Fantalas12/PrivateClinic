using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PrivateClinic.Persistence;
using PrivateClinic.Persistence.Services;
using PrivateClinic.DTO;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace PrivateClinic.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedRecordsController : ControllerBase
    {
        private readonly IPrivateClinicService _service;

        public MedRecordsController(IPrivateClinicService service)
        {
            _service = service;
        }

        // GET: api/MedRecords
        [HttpGet]
        public ActionResult<IEnumerable<MedicalRecordDTO>> GetMedicalRecords()
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return _service
                .GetMedicalRecords(id)
                .Select(medicalrecord => (MedicalRecordDTO)medicalrecord)
                .ToList();
        }



        // GET: api/MedRecords/5
        [HttpGet("{id}")]
        public ActionResult<MedicalRecordDTO> GetMedicalRecord(Int32 id)
        {
            try
            {
                var medicalRecord = _service.GetMedicalRecordById(id);
                return (MedicalRecordDTO)medicalRecord;
            }
            catch (InvalidOperationException)
            {

                return NotFound();
            }
        }

        // POST: api/MedRecords
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [Authorize]
        [HttpPost]
        public ActionResult<MedicalRecordDTO> PostMedicalRecord(MedicalRecordDTO medrecorddto)
        {
            var medrecord = (MedicalRecord)medrecorddto;

            var medicalRecord = _service.CreateMedicalRecord(medrecord);
            if (medicalRecord is null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            else
            {
                return CreatedAtAction(nameof(GetMedicalRecord), new { id = medicalRecord.Id },
                 (MedicalRecordDTO)medicalRecord);
            }
            /*
            var medicalRecord = _service.CreateMedicalRecord((MedicalRecord)medrecorddto);
            if (medicalRecord is null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            else
            {
                return CreatedAtAction(nameof(GetMedicalRecord), new { id = medicalRecord.Id },
                 (MedicalRecordDTO)medicalRecord); 
            } */
        }


        // PUT: api/MedRecords/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [Authorize]
        [HttpPut("{id}")]
        public ActionResult PutMedicalRecord(Int32 id, MedicalRecordDTO medrecorddto)
        {
            if (id != medrecorddto.Id)
            {
                return BadRequest();
            }

            //var medrecord = _service.GetMedicalRecordById(medrecorddto.id);
            //medrecord.PatientId = medrecorddto.PatientId;
            //medrecord.DoctorId = medrecorddto.DoctorId;
            //var Doctor = _service.GetDoctorById(medrecorddto.DoctorId);
            //var Patient = _service.GetPatientById(medrecorddto.PatientId);
            //medrecord
            if (_service.UpdateMedicalRecord((MedicalRecord)medrecorddto))
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
