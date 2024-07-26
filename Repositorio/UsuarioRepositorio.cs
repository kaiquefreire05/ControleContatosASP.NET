using ControleContatos.Database;
using ControleContatos.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleContatos.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {

        //Injetando variável para fazer o controle do banco de dados
        private readonly BancoContext _bancoContext;

        public UsuarioRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }

        // Operações no banco de dados

        public UsuarioModel BuscarPorLogin(string login)
        {
            return _bancoContext.Usuarios.FirstOrDefault(x => x.Login.ToUpper() == login.ToUpper());
        }

        public UsuarioModel Adicionar(UsuarioModel usuario)
        {
            usuario.DataCadastro = DateTime.Now;  // Adicionando data de criação como no banco de dados
            _bancoContext.Usuarios.Add(usuario);
            _bancoContext.SaveChanges();
            return usuario;
        }

        public bool Apagar(int id)
        {
            // Verificando se o usuário existe
            UsuarioModel userDB = ListarPorId(id);
            if (userDB == null) throw new System.Exception("Houve um erro ao deletar contato");

            _bancoContext.Usuarios.Remove(userDB);
            _bancoContext.SaveChanges();
            return true;
        }

        public UsuarioModel Atualizar(UsuarioModel usuario)
        {
            // Verificando se o usuário existe
            UsuarioModel userDB = ListarPorId(usuario.ID);
            if (userDB == null) throw new SystemException("Houve um erro na alteração do usuário");

            userDB.Nome = usuario.Nome;
            userDB.Login = usuario.Login;
            userDB.Email = usuario.Email;
            userDB.Perfil = usuario.Perfil;
            userDB.DataAtualizacao = DateTime.Now;  // Adicionando data de atualização
            _bancoContext.Usuarios.Update(userDB);
            _bancoContext.SaveChanges();

            return userDB;
        }

        public List<UsuarioModel> BuscarTodos()
        {
            return _bancoContext.Usuarios.ToList();
        }

        public UsuarioModel ListarPorId(int id)
        {
            #pragma warning disable CS8603 // Possível retorno nulo
            return _bancoContext.Usuarios.FirstOrDefault(x => x.ID == id);

        }
    }
}
