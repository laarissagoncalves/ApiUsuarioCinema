using System.ComponentModel.DataAnnotations;

namespace UsuariosApi.Request
{
    public class SolicitaResetRequest
    {
        [Required]
        public string Email { get; set; }
    }
}
