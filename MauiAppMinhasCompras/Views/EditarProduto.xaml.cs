using MauiAppMinhasCompras.Models;
namespace MauiAppMinhasCompras.Views;

// A classe EditarProduto é responsável pela lógica de interação com a interface de edição de um produto na aplicação.
public partial class EditarProduto : ContentPage
{
    // O construtor da classe chama o método InitializeComponent para inicializar a interface do usuário.
    public EditarProduto()
    {
        InitializeComponent();
    }

    // Método assíncrono que é executado quando o item "Salvar" da barra de ferramentas é clicado.
    // Ele atualiza o produto com os novos dados inseridos no formulário.
    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            // Obtém o objeto Produto vinculado ao BindingContext da página (provavelmente o produto a ser editado).
            Produto produto_anexado = BindingContext as Produto;

            // Cria uma nova instância de Produto com os dados inseridos pelo usuário no formulário.
            Produto p = new Produto
            {
                Id = produto_anexado.Id, // Mantém o Id original do produto
                Descricao = txt_descricao.Text, // A descrição é obtida do campo de entrada 'txt_descricao'
                Quantidade = Convert.ToDouble(txt_quantidade.Text), // A quantidade é convertida para double a partir do campo 'txt_quantidade'
                Preco = Convert.ToDouble(txt_preco.Text) // O preço é convertido para double a partir do campo 'txt_preco'
            };

            // Chama a função Update da aplicação (provavelmente um método de acesso ao banco de dados) para atualizar o produto.
            await App.Db.Update(p);

            // Exibe uma mensagem de sucesso para o usuário após a atualização bem-sucedida.
            await DisplayAlert("Sucesso!", "Registro Atualizado", "OK");

            // Após o sucesso, retorna para a página anterior na pilha de navegação.
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            // Se ocorrer qualquer erro durante o processo de atualização, exibe uma mensagem de erro.
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }
}
