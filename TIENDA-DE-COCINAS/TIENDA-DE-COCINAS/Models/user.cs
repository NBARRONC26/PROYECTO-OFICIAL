using System.ComponentModel.DataAnnotations;

namespace TIENDA_DE_COCINAS.Models
{
    public class user
    {   [Required]
        [EmailAddress]
        public string email { get;  set; }
        [DataType(DataType.Password)]
        public string password1 { get; set; }
        [DataType(DataType.Password)]
        [Compare("password2",ErrorMessage ="No coinciden las contraseñas")]
        public string password2 { get; set; }
    }
}
