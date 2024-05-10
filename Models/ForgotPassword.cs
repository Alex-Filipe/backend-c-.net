using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Auth.Models
{
    public class ForgotPassword
    {
        [Required(ErrorMessage = "O campo email é obrigatório.")]
        public required string Email { get; set; }
        
        [Required(ErrorMessage = "O campo senha é obrigatório.")]
        public required string Password { get; set; }

        [Required(ErrorMessage = "O campo confirme a senha é obrigatório.")]
        public required string ConfirmPassword { get; set; }
    }
}
