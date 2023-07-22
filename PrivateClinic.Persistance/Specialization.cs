using System.ComponentModel.DataAnnotations;

namespace PrivateClinic.Persistence
{
    public enum SpecializationType {Surgery = 1, Dermatology = 2, Toxicology = 3};

    public class Specialization
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public SpecializationType SpecName { get; set; } = SpecializationType.Surgery;


    }
}
