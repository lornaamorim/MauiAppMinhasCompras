using MauiAppMinhasCompras.Models;
namespace MauiAppMinhasCompras.Views;

public partial class NovoProduto : ContentPage
{
    // Construtor da classe que inicializa o componente da página
    public NovoProduto()
    {
        InitializeComponent();
    }

    // Evento para o item de toolbar "Salvar", responsável por criar e salvar um novo produto
    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            // Criação de um novo objeto Produto e atribuição dos valores das entradas
            Produto p = new Produto
            {
                Descricao = txt_descricao.Text, // Descrição do produto a partir do campo de texto
                Quantidade = Convert.ToDouble(txt_quantidade.Text), // Quantidade a partir do campo de texto, convertendo para double
                Preco = Convert.ToDouble(txt_preco.Text) // Preço a partir do campo de texto, convertendo para double
            };

            // Atualização do produto no banco de dados utilizando a função Update da classe Db
            await App.Db.Update(p);

            // Exibição de uma mensagem de sucesso
            await DisplayAlert("Sucesso!", "Registro Inserido", "OK");
        }
        catch (Exception ex)
        {
            // Em caso de erro, exibe uma mensagem de erro com a exceção
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }
}
