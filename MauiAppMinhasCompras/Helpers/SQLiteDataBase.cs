using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using MauiAppMinhasCompras.Models;

namespace MauiAppMinhasCompras.Helpers
{
    // A classe SQLiteDataBaseHelper é responsável por gerenciar as operações de banco de dados SQLite relacionadas à tabela Produto.
    public class SQLiteDataBaseHelper
    {
        // A variável _conn mantém a conexão assíncrona com o banco de dados SQLite.
        readonly SQLiteAsyncConnection _conn;

        // O construtor da classe recebe um caminho (path) para o banco de dados SQLite e inicializa a conexão com o banco de dados.
        // Também cria a tabela Produto, se ainda não existir.
        public SQLiteDataBaseHelper(string path)
        {
            _conn = new SQLiteAsyncConnection(path);
            _conn.CreateTableAsync<Produto>().Wait(); // Cria a tabela Produto de forma assíncrona
        }

        // O método Update recebe um objeto Produto e atualiza seus dados no banco de dados.
        // Ele utiliza uma consulta SQL para atualizar as colunas Descricao, Quantidade e Preco de um produto com base no seu Id.
        public Task<List<Produto>> Update(Produto p)
        {
            string sql = "UPDATE Produto SET Descricao=?, " +
                         "Quantidade=?, Preco=? WHERE Id=?";
            return _conn.QueryAsync<Produto>(
                sql, p.Descricao, p.Quantidade, p.Preco, p.Id
            );
        }

        // O método Delete recebe um id de produto e remove o produto correspondente no banco de dados.
        // A operação de exclusão é feita usando um filtro na tabela Produto para encontrar o item com o Id correspondente.
        public Task<int> Delete(int id)
        {
            return _conn.Table<Produto>().DeleteAsync(i => i.Id == id); // Deleta o produto com o id fornecido
        }

        // O método GetAll retorna todos os produtos armazenados no banco de dados em forma de lista.
        // Ele utiliza a operação ToListAsync para recuperar todos os registros da tabela Produto.
        public Task<List<Produto>> GetAll()
        {
            return _conn.Table<Produto>().ToListAsync(); // Retorna todos os produtos da tabela Produto
        }

        // O método Search permite buscar produtos por uma palavra-chave na descrição.
        // Ele usa uma consulta SQL com o operador LIKE para buscar por correspondências parciais no campo descricao.
        public Task<List<Produto>> Search(string q)
        {
            string sql = "SELECT * FROM Produto WHERE descricao LIKE '%" + q + "%'";
            return _conn.QueryAsync<Produto>(sql); // Executa a busca e retorna a lista de produtos que correspondem à busca
        }
    }
}


