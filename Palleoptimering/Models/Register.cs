using System.ComponentModel.DataAnnotations;

namespace Palleoptimering.Models
{
    public class Register
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Adgangskode skal være mindst 6 tegn lang")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Adgangskoderne stemmer ikke overens.")]
        public string ConfirmPassword { get; set; }
    }
}
