using ControleContatos.Filters;
using Microsoft.AspNetCore.Mvc;

namespace ControleContatos.Controllers
{
    [PaginaUsuarioLogado] // Adicionando filtro, só vai cair nessa tela quem está com o usuário logado
    public class RestritoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
