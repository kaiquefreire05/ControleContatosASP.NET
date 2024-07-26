using ControleContatos.Filters;
using ControleContatos.Models;
using ControleContatos.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace ControleContatos.Controllers
{
    [PaginaRestritaAdmin] // Adicionando filtro, só entra quem é admin
    public class UsuarioController : Controller
    {
        // Obtendo acesso as classes que fornece as operações CRUD
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public UsuarioController(IUsuarioRepositorio usuarioRepositorio)
        {
            this._usuarioRepositorio = usuarioRepositorio;
        }

        // Views relacionadas a Usuário

        public IActionResult Index()
        {
            List<UsuarioModel> usuarios = _usuarioRepositorio.BuscarTodos();

            // Retornando a lista de usuários na página
            return View(usuarios);

        }

        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Criar(UsuarioModel usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _usuarioRepositorio.Adicionar(usuario);
                    TempData["MensagemSucesso"] = "Usuário foi cadastrado com sucesso";
                    return RedirectToAction("Index");  // Página principal
                }

                return View(usuario);

            }
            catch (System.Exception ex)
            {
                TempData["MensagemErro"] = $"Erro ao cadastrar o usuário. Detalhes erro: {ex.Message}";
                return RedirectToAction("Index"); // Página principal
            }
        }

        public IActionResult ApagarConfirmacao(int id)
        {
            UsuarioModel user = _usuarioRepositorio.ListarPorId(id);  // Enviando o user para a página
            return View(user);
        }

        public IActionResult Apagar(int id)
        {
            try
            {
                bool sucess = _usuarioRepositorio.Apagar(id);

                if (sucess)
                {
                    TempData["MensagemSucesso"] = "Usuário foi apagado com sucesso";
                }
                else
                {
                    TempData["MensagemErro"] = "Usuário não foi apagado";
                }
                return RedirectToAction("Index");  // Retornando a página inicial

            }
            catch (System.Exception ex)
            {
                TempData["MensagemErro"] = $"Usuário não foi apagado. Detalhes erro: {ex.Message}";
                return RedirectToAction("Index");  // Retornando a página inicial
            }
        }

        public IActionResult Editar (int id)
        {
            UsuarioModel user = _usuarioRepositorio.ListarPorId(id);
            return View(user);
        }

        [HttpPost]
        public IActionResult Editar(UsuarioSemSenhaModel userSemSenha)
        {
            try
            {
                UsuarioModel user = null;

                // Verificando se os campos estão devidamente preenchidos
                if (ModelState.IsValid)
                {
                    user = new UsuarioModel()
                    {
                        ID = userSemSenha.ID,
                        Nome = userSemSenha.Nome,
                        Login = userSemSenha.Login,
                        Email = userSemSenha.Email,
                        Perfil = userSemSenha.Perfil
                    };

                    user = _usuarioRepositorio.Atualizar(user);
                    TempData["MensagemSucesso"] = "Usuário editado com sucesso";
                    return RedirectToAction("Index");  // Retornando tela inicial
                }

                return View(user);

            } 
            catch (System.Exception ex)
            {
                TempData["MensagemErro"] = $"Erro ao excluir usuário. Detalhes erro: {ex.Message}";
                return RedirectToAction("Index");  // Retornando tela inicial
            }
        }


    }
}
