using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Auth.Dtos
{
    public class UpdatePasswordUserDto
    {
        [Required(ErrorMessage = "O campo email é obrigatório.")]
        public required string Email { get; set; }
        
        [Required(ErrorMessage = "O campo senha é obrigatório.")]
        [StringLength(255, ErrorMessage = "O campo senha não pode ter mais do que 255 caracteres.")]
        public required string Password { get; set; }

        [Required(ErrorMessage = "O campo confirme a senha é obrigatório.")]
        [StringLength(255, ErrorMessage = "O campo confirme a senha não pode ter mais do que 255 caracteres.")]
        [Compare(nameof(Password), ErrorMessage = "As senhas não coincidem.")]
        public required string ConfirmPassword { get; set; }
    }
}
