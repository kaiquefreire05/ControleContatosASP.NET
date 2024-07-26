using ControleContatos.Models;
using Newtonsoft.Json;

namespace ControleContatos.Helper
{
    public class Sessao : ISessao
    {
        private readonly IHttpContextAccessor _httpContext;

        public Sessao(IHttpContextAccessor contextAccessor)
        {
            _httpContext = contextAccessor;
        }

        public UsuarioModel buscarSessaoUsuario()
        {
            // Obtendo sessão 
            string sessaoUsuario = _httpContext.HttpContext.Session.GetString("sessaoUsuarioLogado");

            // Se a sessão for vazia ou nula
            if (string.IsNullOrEmpty(sessaoUsuario)) return null;

            // Retornando usuário
            #pragma warning disable CS8603
            return JsonConvert.DeserializeObject<UsuarioModel>(sessaoUsuario);

        }

        public void CriarSessaoUsuario(UsuarioModel usuario)
        {
            string valor = JsonConvert.SerializeObject(usuario);  // Transformando objeto em string (json)
            _httpContext.HttpContext.Session.SetString("sessaoUsuarioLogado", valor);
        }

        public void RemoverSessaoUsuario()
        {
            _httpContext.HttpContext.Session.Remove("sessaoUsuarioLogado");
        }

    }
}
