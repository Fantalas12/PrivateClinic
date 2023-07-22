using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace PrivateClinic.Persistence
{
    public class Doctor : ApplicationUser
    {
        public Doctor ()
        {
            Specializations = new HashSet<Specialization>();
        }

        
        /*
        [Key]
        public new int Id { get; set; }
        */
        

        [MaxLength(50)]
        [Required]
        public string Name { get; set; } = string.Empty;

        //public virtual ICollection<Specialization> Specializations { get; set; }

        public virtual ICollection<Specialization> Specializations { get; set; }

        public byte[]? Image { get; set; }

        /*
        [MaxLength(50)]
        [Required]
        public string UserName { get; set; } = string.Empty;
        */

        /*
        [MaxLength(50)]
        [Required]
        public string Password { get; set; } = string.Empty;
        */
    }
}
