using ControleContatos.Enums;
using System.ComponentModel.DataAnnotations;

namespace ControleContatos.Models
{
    public class UsuarioSemSenhaModel
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Digite o nome do usuário")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Digite o login do usuário")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Digite o e-mail do usuário")]
        [EmailAddress(ErrorMessage = "Digite um e-mail válido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Insira o perfil do usuário")]
        public PerfilEnum? Perfil { get; set; }

    }
}
