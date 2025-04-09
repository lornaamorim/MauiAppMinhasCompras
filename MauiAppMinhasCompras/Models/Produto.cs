using SQLite;

namespace MauiAppMinhasCompras.Models
{
    // A classe Produto representa um modelo de dados para os produtos da aplicação.
    // Ela define as propriedades e validações para cada produto, como descrição, quantidade, preço, etc.
    public class Produto
    {
        // A variável _descricao armazena o valor da descrição do produto antes de ser validado.
        string _descricao;

        // O atributo Id é a chave primária da tabela Produto e será gerado automaticamente com um valor único.
        // O AutoIncrement faz com que o valor de Id seja incrementado automaticamente a cada novo produto inserido.
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        // A propriedade Descricao armazena a descrição do produto.
        // Caso o valor da descrição seja nulo, uma exceção será lançada com a mensagem: "Por favor, preencha a descrição".
        public string Descricao
        {
            get => _descricao;
            set
            {
                //a
                // Verifica se o valor da descrição é nulo e lança uma exceção, se necessário.
                if (value == null)
                {
                    throw new Exception(
                        "Por favor, preencha a descrição");
                }

                // Caso contrário, define o valor da descrição.
                _descricao = value;
            }
        }

        // A propriedade Quantidade representa a quantidade de unidades do produto.
        public double Quantidade { get; set; }

        // A propriedade Preco representa o preço unitário do produto.
        public double Preco { get; set; }

        // A propriedade Total calcula o valor total do produto (Quantidade * Preço).
        // Esta é uma propriedade de leitura, que retorna o valor do produto multiplicando a quantidade pelo preço.
        public double Total { get => Quantidade * Preco; }
    } // Fecha Classe
} // Fecha Namespace
