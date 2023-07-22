using System.ComponentModel.DataAnnotations;

namespace PrivateClinic.Persistence
{
    public class Treatment
    {
        [Key]
        public int Id { get; set; }

        public int MedRecordId { get; set; }

        public virtual MedicalRecord? MedicalRecord { get; set; } = null!;

        //public SpecializationType SpecName { get; set; } = SpecializationType.Surgery;

        public string? Description { get; set; }

        public int Price { get; set; }
    }
}
