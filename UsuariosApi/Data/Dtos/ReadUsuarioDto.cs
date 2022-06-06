using System.ComponentModel.DataAnnotations;

namespace UsuariosApi.Data.Dtos
{
    public class ReadUsuarioDto
    {
        [Key]
        [Required]
        public int Id { get; set; }
    }
}
