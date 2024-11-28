
using System.ComponentModel.DataAnnotations;

namespace Palleoptimering.Models
{
    public class Login
    {
        [Required(ErrorMessage = "Brugernavn er påkrævet")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Adgangskode er påkrævet")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}