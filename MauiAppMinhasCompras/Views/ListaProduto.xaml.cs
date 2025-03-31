using MauiAppMinhasCompras.Models;
using System.Collections.ObjectModel;

namespace MauiAppMinhasCompras.Views;

public partial class ListaProduto : ContentPage
{
    // ObservableCollection � usada para permitir a atualiza��o din�mica da lista de produtos na interface do usu�rio.
    ObservableCollection<Produto> lista = new ObservableCollection<Produto>();

    // O construtor da classe inicializa o componente da interface e define a fonte de itens para a ListView.
    public ListaProduto()
    {
        InitializeComponent();
        lst_produtos.ItemsSource = lista;  // Associa a lista � propriedade ItemsSource da ListView para exibi��o dos produtos.
    }

    // M�todo chamado quando a p�gina aparece na tela. Ele carrega todos os produtos do banco de dados e os exibe na lista.
    protected async override void OnAppearing()
    {
        try
        {
            // Obt�m todos os produtos do banco de dados e os adiciona � lista.
            List<Produto> tmp = await App.Db.GetAll();
            tmp.ForEach(i => lista.Add(i));  // Adiciona cada produto � ObservableCollection.
        }
        catch (Exception ex)
        {
            // Exibe uma mensagem de erro caso haja algum problema ao carregar os produtos.
            await DisplayAlert("Ops", ex.Message, "Ok");
        }
    }

    // M�todo para atualizar a lista de produtos quando o usu�rio faz um "pull-to-refresh".
    private async void lst_produtosRefreshing(object sender, EventArgs e)
    {
        try
        {
            lista.Clear();  // Limpa a lista atual.

            // Obt�m todos os produtos do banco de dados novamente para atualizar a lista.
            List<Produto> tmp = await App.Db.GetAll();
            tmp.ForEach(i => lista.Add(i));  // Adiciona os novos produtos � lista.
        }
        catch (Exception ex)
        {
            // Exibe uma mensagem de erro caso haja algum problema ao atualizar os produtos.
            await DisplayAlert("Ops", ex.Message, "Ok");
        }
        finally
        {
            // Ap�s a atualiza��o, desativa a anima��o de "refresh" na ListView.
            lst_produtos.IsRefreshing = false;
        }
    }

    // M�todo chamado quando um item � selecionado na lista. Ele navega at� a p�gina de edi��o do produto.
    private void lst_produtos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        try
        {
            // Obt�m o produto selecionado da lista.
            Produto p = e.SelectedItem as Produto;

            // Navega para a p�gina de edi��o de produto e passa o produto selecionado como contexto de vincula��o.
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

    // M�todo chamado quando o item "Adicionar" da barra de ferramentas � clicado. Ele navega para a p�gina de novo produto.
    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            // Navega para a p�gina de cria��o de um novo produto.
            Navigation.PushAsync(new Views.NovoProduto());
        }
        catch (Exception ex)
        {
            // Exibe uma mensagem de erro caso haja um problema ao tentar adicionar um novo produto.
            DisplayAlert("Ops", ex.Message, "Ok");
        }
    }

    // M�todo chamado quando o texto na barra de pesquisa � alterado. Ele realiza a busca de produtos no banco de dados.
    private async void txt_search_TextChanged(object sender, TextChangedEventArgs e)
    {
        try
        {
            string q = e.NewTextValue;  // Obt�m o texto digitado na barra de pesquisa.

            lista.Clear();  // Limpa a lista atual.

            // Realiza a busca de produtos no banco de dados com base no texto da pesquisa.
            List<Produto> tmp = await App.Db.Search(q);
            tmp.ForEach(i => lista.Add(i));  // Adiciona os produtos encontrados � lista.
        }
        catch (Exception ex)
        {
            // Exibe uma mensagem de erro caso haja um problema durante a pesquisa.
            await DisplayAlert("Ops", ex.Message, "Ok");
        }
    }

    // M�todo chamado quando o item "Somar" da barra de ferramentas � clicado. Ele calcula o total dos produtos na lista.
    private void ToolbarItem_Clicked_1(object sender, EventArgs e)
    {
        // Calcula a soma do total de todos os produtos na lista.
        double soma = lista.Sum(i => i.Total);

        // Exibe o total calculado em uma mensagem de alerta.
        string msg = $"O total � {soma:C}";
        DisplayAlert("Total dos Produtos", msg, "Ok");
    }

    // M�todo chamado quando a op��o "Remover" � clicada no menu de contexto de um item da lista.
    private async void MenuItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            // Obt�m o item de menu que foi clicado.
            MenuItem selecionado = sender as MenuItem;

            // Obt�m o produto associado ao item de menu (usando BindingContext).
            Produto p = selecionado.BindingContext as Produto;

            // Exibe uma mensagem de confirma��o antes de remover o produto.
            bool confirm = await DisplayAlert(
                "Tem certeza?", $"Remover {p.Descricao}?", "Sim", "N�o");

            if (confirm)
            {
                // Se o usu�rio confirmar, o produto � removido do banco de dados e da lista.
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


