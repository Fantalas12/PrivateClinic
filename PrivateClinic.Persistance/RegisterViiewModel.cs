using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PrivateClinic.Persistence
{
    public class RegisterViewModel
    {
        [DisplayName("Felhaználónév")]
        [MaxLength(50)]
        //[RegularExpression(@"^([0-9]{9})$", ErrorMessage = "A polgári névnek legalább két tagúnak kell lenni")]
        public string UserName { get; set; } = string.Empty;

        [DisplayName("Valós név")]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [DisplayName("Jelszó")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [DisplayName("Jelszó megerősítése")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string PasswordRepeat { get; set; } = null!;

        [DisplayName("Telefonszám")]
        //[MaxLength(11)]
        //[DataType(DataType.PhoneNumber)]
        //[Phone] [EmailAddress]
        [RegularExpression(@"^(06[0-9]{9})$", ErrorMessage = "A telefonszámnak 06-al kell kezdődnie, csak számokat tartalmazhat és 9 számjegyű!")]
        public string PhoneNumber  { get; set; } = null!;

        [DisplayName("Lakcím")]
        [MaxLength(50)]
        public string Address { get; set; } = string.Empty;
    }
}