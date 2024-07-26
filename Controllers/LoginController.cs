using ControleContatos.Models;
using ControleContatos.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace ControleContatos.Controllers
{
    public class LoginController : Controller
    {
        // Obtendo acceso ao banco de dados de usuarios

        private readonly IUsuarioRepositorio _usuariorepositorio;
        public LoginController(IUsuarioRepositorio usuarioRepositorio)
        {
            this._usuariorepositorio = usuarioRepositorio;
        }

        // Métodos da View
        public IActionResult IndexLogin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Entrar(LoginModel loginModel)
        {
            try
            {
                // Se os campos forem preenchidos
                if (ModelState.IsValid)
                {
                    // Buscando modelo no banco de dados
                    UsuarioModel userModel = _usuariorepositorio.BuscarPorLogin(loginModel.Login);

                    // Se o usuário for encontrado entra na condição
                    if (userModel != null)
                    {
                        // Se a senha for válida (true), permite acesso
                        if (userModel.SenhaValida(loginModel.Senha))
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        // Senão o usuário está correto, mas a senha é inválida
                        TempData["MensagemErro"] = "Senha do usuário é inválida. Por favor tente novamente.";
                    }

                    // Mensagem de login e senha diferentes
                    TempData["MensagemErro"] = "Usuario e/ou Senha inválido(s). Por favor tente novamente.";

                }

                return View("IndexLogin");  // Página inicial de login    
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = $"Erro ao fazer o Login. Detalhe erro: {ex.Message}";
                return RedirectToAction("IndexLogin");
            }
        }

    }
}
