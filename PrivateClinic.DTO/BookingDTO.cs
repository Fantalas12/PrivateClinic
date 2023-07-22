using System;
using System.Collections.Generic;
using System.Text;
using PrivateClinic.Persistence;

namespace PrivateClinic.DTO
{


    public class BookingDTO
    {
        static Func<Int32, String> SpecIdConverter = specialization =>
        {

            switch (specialization)
            {
                case 1: return "Toxikológia";
                case 2: return "Bőrgyógyászat";
                case 3: return "Sebészet";
                default: return "?";
            }
        };

        public int Id { get; set; }

        public string PatientID { get; set; } = null!;

        public string DoctorID { get; set; } = null!;

        public Int32? SpecializationId { get; set; }

        public DateTime DateTime { get; set; } = DateTime.Now;

        public String? Comment { get; set; }

        public string PatientName { get; set; } = null!;

        public String SpecName { get; set; } = null!;

        public static explicit operator Booking(BookingDTO dto) => new Booking
        {
            Id = dto.Id,
            PatientID = dto.PatientID,
            DoctorID = dto.DoctorID,
            SpecializationId = dto.SpecializationId,
            DateTime = dto.DateTime,
            Comment = dto.Comment,
            //PatientName = dto.PatientName,
        };

        public static explicit operator BookingDTO(Booking i) => new BookingDTO
        {
            Id = i.Id,
            PatientID = i.PatientID,
            DoctorID = i.DoctorID,
            SpecializationId = i.SpecializationId,
            DateTime = i.DateTime,
            Comment = i.Comment,
            PatientName = i.Patient.Name,
            SpecName = SpecIdConverter(i.SpecializationId ?? 0)
        };


        /* public static explicit operator MedicalRecordDTO(BookingDTO dto) => new MedicalRecordDTO
        {
            //Id = dto.Id,
            PatientId = dto.PatientID,
            DoctorId = dto.DoctorID,
            DateTime = dto.DateTime,
        };

        public static explicit operator BookingDTO(MedicalRecordDTO dto) => new BookingDTO
        {
            //Id = dto.Id,
            PatientID = dto.PatientId,
            DoctorID = dto.DoctorId,
            DateTime = dto.DateTime,
        }; */

    }
}