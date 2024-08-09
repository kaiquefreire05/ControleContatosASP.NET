using System.ComponentModel.DataAnnotations;

namespace ControleContatos.Models
{
    public class RedefinirSenhaModel
    {
        [Required(ErrorMessage = "Digite seu login")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Digite seu email")]
        public string Email { get; set; }
    }
}
