using MauiAppMinhasCompras.Models;
namespace MauiAppMinhasCompras.Views;

public partial class NovoProduto : ContentPage
{
    // Construtor da classe que inicializa o componente da p�gina
    public NovoProduto()
    {
        InitializeComponent();
    }

    // Evento para o item de toolbar "Salvar", respons�vel por criar e salvar um novo produto
    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            // Cria��o de um novo objeto Produto e atribui��o dos valores das entradas
            Produto p = new Produto
            {
                Descricao = txt_descricao.Text, // Descri��o do produto a partir do campo de texto
                Quantidade = Convert.ToDouble(txt_quantidade.Text), // Quantidade a partir do campo de texto, convertendo para double
                Preco = Convert.ToDouble(txt_preco.Text) // Pre�o a partir do campo de texto, convertendo para double
            };

            // Atualiza��o do produto no banco de dados utilizando a fun��o Update da classe Db
            await App.Db.Update(p);

            // Exibi��o de uma mensagem de sucesso
            await DisplayAlert("Sucesso!", "Registro Inserido", "OK");
        }
        catch (Exception ex)
        {
            // Em caso de erro, exibe uma mensagem de erro com a exce��o
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }
}
