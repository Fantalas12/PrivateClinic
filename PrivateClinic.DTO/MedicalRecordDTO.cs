using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrivateClinic.Persistence;


namespace PrivateClinic.DTO
{
    public class MedicalRecordDTO
    {
        public Int32 Id { get; set; }

        public String PatientId { get; set; } = null!;

        public String DoctorId { get; set; } = null!;

        public DateTime DateTime { get; set; }

        public Int32 SumPrice { get; set; }

        public String PatientName { get; set; } = null!;

        public static explicit operator MedicalRecord(MedicalRecordDTO dto) => new MedicalRecord
        {
            Id = dto.Id,
            PatientId = dto.PatientId,
            DoctorId = dto.DoctorId,
            DateTime = dto.DateTime,
            //SumPrice = dto.SumPrice
        };

        public static explicit operator MedicalRecordDTO(MedicalRecord i) => new MedicalRecordDTO
        {
            Id = i.Id,
            PatientId = i.PatientId,
            DoctorId = i.DoctorId,
            DateTime = i.DateTime,
            PatientName = i.Patient?.Name ?? "",
            SumPrice = i.SumPrice
        };

        
        public static explicit operator MedicalRecordDTO(BookingDTO dto) => new MedicalRecordDTO
        {
            Id = dto.Id,
            PatientId = dto.PatientID,
            DoctorId = dto.DoctorID,
            DateTime = dto.DateTime,
            PatientName = dto.PatientName
        };

        public static explicit operator BookingDTO(MedicalRecordDTO dto) => new BookingDTO
        {
            Id = dto.Id,
            PatientID = dto.PatientId,
            DoctorID = dto.DoctorId,
            DateTime = dto.DateTime,
            PatientName = dto.PatientName
        };
        

    }
}
