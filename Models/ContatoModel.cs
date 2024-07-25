using System.ComponentModel.DataAnnotations;

namespace ControleContatos.Models
{
    public class ContatoModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Digite o nome do contato")] // Campo obrigatório no database
        public string Name { get; set; }
        [Required(ErrorMessage = "Digite o e-mail do contato")]  // Campo obrigatório no database
        [EmailAddress(ErrorMessage = "O e-mail informado não é válido")]  // Validando formato email
        public string Email { get; set; }
        [Required(ErrorMessage = "Digite o celular do contato")]  // Campo obrigatório no database
        [Phone(ErrorMessage = "O celular informado não é válido")]  // Validando formato celular
        public string Celular{ get; set; }
    }

}
