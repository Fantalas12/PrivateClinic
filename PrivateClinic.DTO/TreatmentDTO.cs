using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrivateClinic.Persistence;

namespace PrivateClinic.DTO
{
    public class TreatmentDTO
    {
        public int Id { get; set; }

        public int MedRecordId { get; set; }

        public String? Description { get; set; }

        public Int32 Price { get; set; }


        public static explicit operator Treatment(TreatmentDTO dto) => new Treatment
        {
            Id = dto.Id,
            MedRecordId = dto.MedRecordId,
            Description = dto.Description,
            Price = dto.Price

        };

        public static explicit operator TreatmentDTO(Treatment i) => new TreatmentDTO
        {
            Id = i.Id,
            MedRecordId = i.MedRecordId,
            Description = i.Description,
            Price = i.Price
        };



    }
}
