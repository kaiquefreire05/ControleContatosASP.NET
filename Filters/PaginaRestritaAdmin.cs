using ControleContatos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace ControleContatos.Filters
{
    // Define o filtro de ação 'PaginaUsuarioLogado' que herda de ActionFilterAttribute
    public class PaginaRestritaAdmin : ActionFilterAttribute
    {
        // Sobrescreve o método OnActionExecuted da classe ActionFilterAttribute
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            // Obtém o valor da sessão do usuário logado usando a chave "sessaoUsuarioLogado"
            string sessaoUsuario = context.HttpContext.Session.GetString("sessaoUsuarioLogado");

            // Verifica se a sessão é nula ou vazia
            if (string.IsNullOrEmpty(sessaoUsuario))
            {
                // Redireciona para a página de login se a sessão do usuário não estiver presente
                context.Result = new RedirectToRouteResult(new RouteValueDictionary{{"controller", "Login"},{"action", "Index"}});
            }
            else
            {
                // Desserializa a sessão em um objeto UsuarioModel
                UsuarioModel usuario = JsonConvert.DeserializeObject<UsuarioModel>(sessaoUsuario);

                // Verifica se a desserialização resultou em um usuário nulo
                if (usuario == null)
                {
                    // Redireciona para a página de login se o usuário desserializado for nulo
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary
                    {
                        {"Controller", "Login"},
                        {"action", "Index"}
                    });
                }

                // Verificando se o perfil logado é administrador
                if (usuario.Perfil != ControleContatos.Enums.PerfilEnum.Admin)
                {
                    // Redireciona para a página de acesso restrito
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Restrito" }, { "action", "Index" } });
                }
            }

            // Chama o método base para garantir que a lógica da classe base seja executada
            base.OnActionExecuted(context);
        }
    }
}