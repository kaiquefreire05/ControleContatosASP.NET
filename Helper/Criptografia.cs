using System.Security.Cryptography;
using System.Text;

namespace ControleContatos.Helper
{
    public static class Criptografia // static para não precisar ser instânciada
    {
        public static string GerarHash(this string valor)  // Método de extensão que gera um hash do valor da string
        {
            var hash = SHA1.Create();  // Cria uma instância do algoritmo SHA1 para gerar o hash
            var array = new ASCIIEncoding().GetBytes(valor);  // Converte o valor da string em um array de bytes usando codificação ASCII
            array = hash.ComputeHash(array);  // Computa o hash do array de bytes

            var strHexa = new StringBuilder();  // Usa StringBuilder para construir a string hexadecimal do hash
            foreach (var item in array)  // Itera sobre cada byte do hash
            {
                strHexa.Append(item.ToString("x2"));  // Converte o byte para uma representação hexadecimal de 2 dígitos e adiciona ao StringBuilder
            }
            return strHexa.ToString();  // Retorna a string hexadecimal do hash
        }
    }
}
