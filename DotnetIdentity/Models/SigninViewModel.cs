using System.ComponentModel.DataAnnotations;

namespace DotnetIdentity.Models
{
    public class SigninViewModel
    {
        [Required(ErrorMessage ="User name must be provided.")]
        [DataType(DataType.EmailAddress)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password must be provided.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        
    }
}
