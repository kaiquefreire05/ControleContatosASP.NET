using ControleContatos.Database;
using ControleContatos.Models;

namespace ControleContatos.Repositorio
{
    public class ContatoRepositorio : IContatoRepositorio
    {   
        // Variável privada convenção usar o "_"
        private readonly BancoContext _bancoContext;

        public ContatoRepositorio(BancoContext bancoContext)  // Injetando o BancoContext
        {
            _bancoContext = bancoContext;
        }

        // Gravar no database
        public ContatoModel Adicionar(ContatoModel contato)
        {
            _bancoContext.Contatos.Add(contato);
            _bancoContext.SaveChanges();
            return contato;

        }

        // Listar contatos no database
        public List<ContatoModel> BuscarTodos()
        {
            return _bancoContext.Contatos.ToList();
        }
    }
}
