using MauiAppMinhasCompras.Models;
namespace MauiAppMinhasCompras.Views;

// A classe EditarProduto � respons�vel pela l�gica de intera��o com a interface de edi��o de um produto na aplica��o.
public partial class EditarProduto : ContentPage
{
    // O construtor da classe chama o m�todo InitializeComponent para inicializar a interface do usu�rio.
    public EditarProduto()
    {
        InitializeComponent();
    }

    // M�todo ass�ncrono que � executado quando o item "Salvar" da barra de ferramentas � clicado.
    // Ele atualiza o produto com os novos dados inseridos no formul�rio.
    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            // Obt�m o objeto Produto vinculado ao BindingContext da p�gina (provavelmente o produto a ser editado).
            Produto produto_anexado = BindingContext as Produto;

            // Cria uma nova inst�ncia de Produto com os dados inseridos pelo usu�rio no formul�rio.
            Produto p = new Produto
            {
                Id = produto_anexado.Id, // Mant�m o Id original do produto
                Descricao = txt_descricao.Text, // A descri��o � obtida do campo de entrada 'txt_descricao'
                Quantidade = Convert.ToDouble(txt_quantidade.Text), // A quantidade � convertida para double a partir do campo 'txt_quantidade'
                Preco = Convert.ToDouble(txt_preco.Text) // O pre�o � convertido para double a partir do campo 'txt_preco'
            };

            // Chama a fun��o Update da aplica��o (provavelmente um m�todo de acesso ao banco de dados) para atualizar o produto.
            await App.Db.Update(p);

            // Exibe uma mensagem de sucesso para o usu�rio ap�s a atualiza��o bem-sucedida.
            await DisplayAlert("Sucesso!", "Registro Atualizado", "OK");

            // Ap�s o sucesso, retorna para a p�gina anterior na pilha de navega��o.
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            // Se ocorrer qualquer erro durante o processo de atualiza��o, exibe uma mensagem de erro.
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }
}
