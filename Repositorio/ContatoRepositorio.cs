using ControleContatos.Database;
using ControleContatos.Models;

namespace ControleContatos.Repositorio
{
    public class ContatoRepositorio : IContatoRepositorio
    {   
        // Variável privada convenção usar o "_"
        private readonly BancoContext _context;

        public ContatoRepositorio(BancoContext bancoContext)  // Injetando o BancoContext
        {
            _context = bancoContext;
        }

        // Gravar no database
        public ContatoModel Adicionar(ContatoModel contato)
        {
            _context.Contatos.Add(contato);
            _context.SaveChanges();
            return contato;

        }

        // Apagar contato
        public bool Apagar(int id)
        {   
            // Buscando por ID e verificando se ele existe
            ContatoModel contatoDB = ListarPorId(id);
            if (contatoDB == null) throw new System.Exception("Houve um erro na deleção do contato");

            _context.Contatos.Remove(contatoDB);
            _context.SaveChanges();
            return true;

        }

        // Atualizar contato
        public ContatoModel Atualizar(ContatoModel contato)
        {
            // Buscando o contato existente no banco de dados pelo ID
            ContatoModel contatoDB = ListarPorId(contato.Id);

            // Se o contato não existir explode exceção
            if (contatoDB == null) throw new System.Exception("Houve um erro na atualização do contato");

            // Atualizando os campos com novos valores 
           
            contatoDB.Name = contato.Name;
            contatoDB.Email = contato.Email;
            contatoDB.Celular = contato.Celular;
            _context.Contatos.Update(contatoDB);
            _context.SaveChanges();

            // Retornando o contato atualizado
            return contatoDB;  
            
        }

        // Listar contatos no database
        public List<ContatoModel> BuscarTodos()
        {
            return _context.Contatos.ToList();
        }

        // Listar contato por ID
        public ContatoModel ListarPorId(int id)
        {
            #pragma warning disable CS8603 // Possível retorno de referência nula.
            return _context.Contatos.FirstOrDefault(x => x.Id == id);
            
        }
    }
}
