using ControleContatos.Helper;
using ControleContatos.Models;
using ControleContatos.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace ControleContatos.Controllers
{
    public class LoginController : Controller
    {
        // Injetando dependências de sessão e banco de dados
        private readonly ISessao _sessao;
        private readonly IUsuarioRepositorio _usuariorepositorio;
        public LoginController(IUsuarioRepositorio usuarioRepositorio, ISessao isessao)
        {
            this._usuariorepositorio = usuarioRepositorio;
            this._sessao = isessao;
        }

        // Métodos da View
        public IActionResult IndexLogin()
        {
            // Se o usuário estiver logado redirecionar para home
            if (_sessao.buscarSessaoUsuario() !=null) return RedirectToAction("Index", "Home");
            return View();
        }

        public IActionResult RedefinirSenha()
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
                            _sessao.CriarSessaoUsuario(userModel);  // Criando sessão
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

        [HttpPost]
        public IActionResult EnviarLinkParaRedefinirSenha(RedefinirSenhaModel redefinirSenhaModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioModel usuario = _usuariorepositorio.BuscarPorEmailLogin(redefinirSenhaModel.Email, redefinirSenhaModel.Login);
                    if (usuario != null)
                    {
                        string novaSenha = usuario.GerarNovaSenha();
                        TempData["MensagemSucesso"] = $"Enviamos para o seu email cadastrado uma nova senha.";
                        return RedirectToAction("IndexLogin", "Login");
                    }
                    TempData["MensagemErro"] = $"Não conseguimos redefinir sua senha. Por favor, verifique os dados informados.";
                }       
                return View("IndexLogin");
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos redefinir sua senha, detalhe erro: {ex.Message}";
                return RedirectToAction("IndexLogin");
            }
        }

        public IActionResult Sair()
        {
            _sessao.RemoverSessaoUsuario();
            return RedirectToAction("IndexLogin", "Login");
        }

    }
}
