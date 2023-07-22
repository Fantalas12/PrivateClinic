using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
/*using System.Da */

namespace PrivateClinic.Persistence
{
    public class Booking
    {

        [Key]
        public int Id { get; set; }
        
        public string PatientID { get; set; } = null!;

        public virtual Patient Patient { get; set; } = null!;

        [Required]
        public DateTime DateTime { get; set; } = DateTime.Now;

        public string DoctorID { get; set; } = null!;

        public virtual Doctor Doctor { get; set; } = null!;

        
        //public SpecializationType PureSpecialization { get; set; } = SpecializationType.Surgery;
        
        
        public Int32? SpecializationId { get; set; }

        public virtual Specialization Specialization { get; set; } = null!;



        [MaxLength(250)]
        //[AllowNull]
        public String? Comment { get; set; }





    }
}
