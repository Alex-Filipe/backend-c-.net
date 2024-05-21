using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Auth.Dtos
{
    public class CreateUserDto
    {
        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        [StringLength(255, ErrorMessage = "O campo Nome não pode ter mais do que 255 caracteres.")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "O campo email é obrigatório.")]
        [StringLength(255, ErrorMessage = "O campo Email não pode ter mais do que 255 caracteres.")]
        [EmailAddress(ErrorMessage = "O campo Email deve conter um endereço de e-mail válido.")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "O campo senha é obrigatório.")]
        [StringLength(255, ErrorMessage = "O campo Senha não pode ter mais do que 255 caracteres.")]
        public required string Password { get; set; }

        [Required(ErrorMessage = "O campo role é obrigatório.")]
        public required int Id_role { get; set; }
    }
}
