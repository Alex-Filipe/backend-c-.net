using System.ComponentModel.DataAnnotations;

namespace Auth.Dtos
{
    public class CreateRoleDto
    {
        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        [StringLength(255, ErrorMessage = "O campo Nome não pode ter mais do que 255 caracteres.")]
        public required string Name { get; set; }
    }
}
