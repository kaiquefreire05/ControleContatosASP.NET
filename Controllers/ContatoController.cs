using ControleContatos.Models;
using ControleContatos.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace ControleContatos.Controllers
{
    public class ContatoController : Controller
    {
        // Obtendo acesso a classe que faz operações CRUD

        private readonly IContatoRepositorio _contatoRepositorio;

        public ContatoController(IContatoRepositorio contatoRepositorio)
        {
           _contatoRepositorio = contatoRepositorio;
        }

        // View inicial que inicializar com todos os contatos
        public IActionResult Index()
        {
            List<ContatoModel> contatos = _contatoRepositorio.BuscarTodos();
            return View(contatos);
        }

        // Método que redireciona para a página Criar
        public IActionResult Criar()
        {
            return View();
        }

        // Método que redireciona para a página Alterar, junto com o ID para ser alterado (id não é alterável)
        public IActionResult Editar(int id)
        {

            ContatoModel contato = _contatoRepositorio.ListarPorId(id); // Buscando o contato
            return View(contato);  // Iniciando a view já com as informações do contato
        }

        // Página de confirmação de exclusão
        public IActionResult ApagarConfirmacao(int id)  // ID que vai ser apagado
        {
            ContatoModel contato = _contatoRepositorio.ListarPorId(id); // Buscando o contato
            return View(contato);  // Dando acesso pela view
        }
        
        // Método de apagar contato
        public IActionResult Apagar(int id)
        {
            try
            {
                bool sucess = _contatoRepositorio.Apagar(id);
                if (sucess)
                {
                    TempData["MensagemSucesso"] = "Contato excluído com sucesso";
                    
                }
                else
                {
                    TempData["MensagemErro"] = "Erro. Contato não foi excluído";

                }
                return RedirectToAction("Index");  // Retornando a página inicial (index)

            }
            catch (SystemException ex)
            {
                TempData["MensagemErro"] = $"Erro. Contato não foi excluído, detalhes: {ex.Message}";
                return RedirectToAction("Index");  // Retornando a página inicial (index)
            }
            

        }

        // Método de criar contato
        [HttpPost]
        public IActionResult Criar(ContatoModel contato)
        {
            try
            {
                // Verificando se não existe erro
                if (ModelState.IsValid)
                {
                    _contatoRepositorio.Adicionar(contato);
                    TempData["MensagemSucesso"] = "Contato cadastrado com sucesso";
                    return RedirectToAction("Index");  // página principal
                }

                return View(contato);
            }
            catch (System.Exception ex)
            {
                TempData["MensagemErro"] = $"Contato não foi cadastrado. Tente novamente, detalhe erro: {ex.Message}";
                return RedirectToAction("Index"); // Voltando a mesma página
            }

        }

        // Método de alterar contato
        [HttpPost]
        public IActionResult Alterar(ContatoModel contato)
        {   
            try
            {
                if (ModelState.IsValid)
                {
                    _contatoRepositorio.Atualizar(contato);
                    TempData["MensagemSucesso"] = "Contato foi alterado com sucesso.";
                    return RedirectToAction("Index");
                }

                return View("Editar", contato);  // Forçando retorno para a view editar (alterar não existe)
            }
            catch (System.Exception ex)
            {
                TempData["MensagemErro"] = $"Contato não foi alterado. Tente novamente, detalhe erro: {ex.Message}";
                return RedirectToAction("Index"); // Voltando a mesma página
            }
            
        }

    }
}
