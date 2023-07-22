using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrivateClinic.Persistence
{
    public class MedicalRecord
    {

        public MedicalRecord ()
        {
            Treatments = new HashSet<Treatment> ();
        }

        [Key]
        public int Id { get; set; }

        //public int TreatmentId { get; set; }

        public virtual ICollection<Treatment> Treatments { get; set; }

        public string PatientId { get; set; } = null!;

        public virtual Patient? Patient { get; set; } = null!;

        public string DoctorId { get; set; } = null!;

        public virtual Doctor? Doctor { get; set; } = null!;

        public DateTime DateTime { get; set; } = DateTime.Now;

        [NotMapped]
        public int SumPrice => Treatments.Sum(t => t.Price);
    }
}
