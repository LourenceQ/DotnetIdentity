using System.ComponentModel.DataAnnotations;

namespace DotnetIdentity.Models
{
    public class SignupViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "Endereço de email é inválido.")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password, ErrorMessage = "Senha incorreta.")]
        public string Password { get; set; }
        [Required]
        public string Role { get; set; }
    }
}
