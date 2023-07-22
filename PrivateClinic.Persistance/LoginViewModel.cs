using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PrivateClinic.Persistence
{
    public class LoginViewModel
    {
        [DisplayName("Név")]
        public string UserName { get; set; } = null!;

        [DisplayName("Jelszó")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
    }
}
