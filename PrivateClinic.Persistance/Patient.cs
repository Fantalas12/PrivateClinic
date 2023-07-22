using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;


namespace PrivateClinic.Persistence
{
    public class Patient : ApplicationUser
    {
        //A következő mezőket az identityUserből való származtatással már megkapjuk
        /*
        [Key]
        public String Id { get; set; }
  
        [MaxLength(50)]
        [Required]
        public string UserName { get; set; } = string.Empty;

        [MaxLength(50)]
        [Required]
        [DataType(DataType.PhoneNumber)]
        //[RegularExpression(@"^(\d{11})$", ErrorMessage = "Rossz mobilszám")]
        public string PhoneNumber { get; set; } = string.Empty;

        [MaxLength(50)]
        [Required]
        public string Password { get; set; } = string.Empty;
        */


        [MaxLength(50)]
        [Required]
        public string Name { get; set; } = string.Empty;

        [MaxLength(50)]
        [Required]
        public string Address { get; set; } = string.Empty;



    }
}
