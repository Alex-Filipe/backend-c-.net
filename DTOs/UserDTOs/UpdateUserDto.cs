using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Auth.Dtos
{
    public class UpdateUserDto
    {
        public int Id { get; set; }

        [Column("nome")]
        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        [StringLength(255, ErrorMessage = "O campo Nome não pode ter mais do que 255 caracteres.")]
        public required string Name { get; set; }

        [Column("email")]
        [Required(ErrorMessage = "O campo email é obrigatório.")]
        [StringLength(255, ErrorMessage = "O campo Email não pode ter mais do que 255 caracteres.")]
        [EmailAddress(ErrorMessage = "O campo Email deve conter um endereço de e-mail válido.")]
        public required string Email { get; set; }

        [Column("id_role")]
        [Required(ErrorMessage = "O campo role é obrigatório.")]
        public required int Id_role { get; set; }
    }
}
