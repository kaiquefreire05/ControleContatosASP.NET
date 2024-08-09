using ControleContatos.Models;

namespace ControleContatos.Repositorio
{
    public interface IUsuarioRepositorio
    {
        List<UsuarioModel> BuscarTodos();

        UsuarioModel Adicionar(UsuarioModel usuario);

        UsuarioModel ListarPorId(int id);

        UsuarioModel Atualizar (UsuarioModel usuario);

        bool Apagar(int id);

        UsuarioModel BuscarPorLogin(string login);

        UsuarioModel BuscarPorEmailLogin(string email, string login);
    }
}
