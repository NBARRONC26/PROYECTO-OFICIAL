using System.ComponentModel.DataAnnotations;

namespace TIENDA_DE_COCINAS.Models
{
    public class user2
    {
        
            [Required]
            [EmailAddress]
            public string email { get; set; }
            [DataType(DataType.Password)]
            public string password1 { get; set; }
            [Display(Name = "recordar contraseña")]
            public bool recordar{ get; set; }
        }
}
