using MauiAppMinhasCompras.Models;
using System.Collections.ObjectModel;

namespace MauiAppMinhasCompras.Views;

public partial class ListaProduto : ContentPage
{
    // ObservableCollection é usada para permitir a atualização dinâmica da lista de produtos na interface do usuário.
    ObservableCollection<Produto> lista = new ObservableCollection<Produto>();

    // O construtor da classe inicializa o componente da interface e define a fonte de itens para a ListView.
    public ListaProduto()
    {
        InitializeComponent();
        lst_produtos.ItemsSource = lista;  // Associa a lista à propriedade ItemsSource da ListView para exibição dos produtos.
    }

    // Método chamado quando a página aparece na tela. Ele carrega todos os produtos do banco de dados e os exibe na lista.
    protected async override void OnAppearing()
    {
        try
        {
            // Obtém todos os produtos do banco de dados e os adiciona à lista.
            List<Produto> tmp = await App.Db.GetAll();
            tmp.ForEach(i => lista.Add(i));  // Adiciona cada produto à ObservableCollection.
        }
        catch (Exception ex)
        {
            // Exibe uma mensagem de erro caso haja algum problema ao carregar os produtos.
            await DisplayAlert("Ops", ex.Message, "Ok");
        }
    }

    // Método para atualizar a lista de produtos quando o usuário faz um "pull-to-refresh".
    private async void lst_produtosRefreshing(object sender, EventArgs e)
    {
        try
        {
            lista.Clear();  // Limpa a lista atual.

            // Obtém todos os produtos do banco de dados novamente para atualizar a lista.
            List<Produto> tmp = await App.Db.GetAll();
            tmp.ForEach(i => lista.Add(i));  // Adiciona os novos produtos à lista.
        }
        catch (Exception ex)
        {
            // Exibe uma mensagem de erro caso haja algum problema ao atualizar os produtos.
            await DisplayAlert("Ops", ex.Message, "Ok");
        }
        finally
        {
            // Após a atualização, desativa a animação de "refresh" na ListView.
            lst_produtos.IsRefreshing = false;
        }
    }

    // Método chamado quando um item é selecionado na lista. Ele navega até a página de edição do produto.
    private void lst_produtos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        try
        {
            // Obtém o produto selecionado da lista.
            Produto p = e.SelectedItem as Produto;

            // Navega para a página de edição de produto e passa o produto selecionado como contexto de vinculação.
            Navigation.PushAsync(new Views.EditarProduto
            {
                BindingContext = p,
            });
        }
        catch (Exception ex)
        {
            // Exibe uma mensagem de erro caso haja um problema ao selecionar o item.
            DisplayAlert("Ops", ex.Message, "Ok");
        }
    }

    // Método chamado quando o item "Adicionar" da barra de ferramentas é clicado. Ele navega para a página de novo produto.
    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            // Navega para a página de criação de um novo produto.
            Navigation.PushAsync(new Views.NovoProduto());
        }
        catch (Exception ex)
        {
            // Exibe uma mensagem de erro caso haja um problema ao tentar adicionar um novo produto.
            DisplayAlert("Ops", ex.Message, "Ok");
        }
    }

    // Método chamado quando o texto na barra de pesquisa é alterado. Ele realiza a busca de produtos no banco de dados.
    private async void txt_search_TextChanged(object sender, TextChangedEventArgs e)
    {
        try
        {
            string q = e.NewTextValue;  // Obtém o texto digitado na barra de pesquisa.

            lista.Clear();  // Limpa a lista atual.

            // Realiza a busca de produtos no banco de dados com base no texto da pesquisa.
            List<Produto> tmp = await App.Db.Search(q);
            tmp.ForEach(i => lista.Add(i));  // Adiciona os produtos encontrados à lista.
        }
        catch (Exception ex)
        {
            // Exibe uma mensagem de erro caso haja um problema durante a pesquisa.
            await DisplayAlert("Ops", ex.Message, "Ok");
        }
    }

    // Método chamado quando o item "Somar" da barra de ferramentas é clicado. Ele calcula o total dos produtos na lista.
    private void ToolbarItem_Clicked_1(object sender, EventArgs e)
    {
        // Calcula a soma do total de todos os produtos na lista.
        double soma = lista.Sum(i => i.Total);

        // Exibe o total calculado em uma mensagem de alerta.
        string msg = $"O total é {soma:C}";
        DisplayAlert("Total dos Produtos", msg, "Ok");
    }

    // Método chamado quando a opção "Remover" é clicada no menu de contexto de um item da lista.
    private async void MenuItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            // Obtém o item de menu que foi clicado.
            MenuItem selecionado = sender as MenuItem;

            // Obtém o produto associado ao item de menu (usando BindingContext).
            Produto p = selecionado.BindingContext as Produto;

            // Exibe uma mensagem de confirmação antes de remover o produto.
            bool confirm = await DisplayAlert(
                "Tem certeza?", $"Remover {p.Descricao}?", "Sim", "Não");

            if (confirm)
            {
                // Se o usuário confirmar, o produto é removido do banco de dados e da lista.
                await App.Db.Delete(p.Id);
                lista.Remove(p);  // Remove o produto da ObservableCollection, o que atualiza a UI automaticamente.
            }
        }
        catch (Exception ex)
        {
            // Exibe uma mensagem de erro caso haja um problema ao tentar remover o produto.
            await DisplayAlert("Ops", ex.Message, "Ok");
        }
    }
}


