
# MauiAppMinhasCompras

Este é um aplicativo simples de gerenciamento de compras desenvolvido utilizando .NET MAUI, SQLite e C#. O objetivo do aplicativo é permitir que os usuários registrem e gerenciem produtos de compras, com funcionalidades de adicionar, editar, remover e visualizar produtos, além de calcular o total dos itens.

## Funcionalidades

- **Listar Produtos**: Exibe uma lista de produtos com suas descrições, quantidades, preços e totais calculados.
- **Adicionar Novo Produto**: Permite ao usuário adicionar um novo produto com descrição, quantidade e preço.
- **Editar Produto**: Permite editar as informações de um produto existente.
- **Remover Produto**: Exclui um produto da lista.
- **Buscar Produtos**: Permite buscar produtos por descrição.
- **Cálculo do Total**: Calcula o total acumulado de todos os produtos listados.

## Tecnologias Utilizadas

- **.NET MAUI**: Framework multiplataforma para desenvolvimento de aplicativos móveis e desktop.
- **SQLite**: Banco de dados local para armazenar as informações dos produtos.
- **C#**: Linguagem de programação utilizada para o desenvolvimento do aplicativo.

## Estrutura do Projeto

O projeto é composto por várias partes que lidam com o banco de dados SQLite e a interface de usuário. A seguir está uma visão geral dos principais arquivos e funcionalidades.

### 1. **SQLiteDataBaseHelper**

Esta classe gerencia a conexão com o banco de dados SQLite e realiza operações CRUD (Create, Read, Update, Delete) para manipulação dos dados.

```csharp
public class SQLiteDataBaseHelper
{
    // Métodos principais:
    public Task<List<Produto>> Update(Produto p) { ... }
    public Task<int> Delete(int id) { ... }
    public Task<List<Produto>> GetAll() { ... }
    public Task<List<Produto>> Search(string q) { ... }
}
```

### 2. **Produto**

Modelo que representa os dados de um produto. Contém propriedades como `Id`, `Descricao`, `Quantidade`, `Preco` e `Total`.

```csharp
public class Produto
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Descricao { get; set; }
    public double Quantidade { get; set; }
    public double Preco { get; set; }
    public double Total { get => Quantidade * Preco; }
}
```

### 3. **App.xaml.cs**

A classe `App` inicializa o aplicativo e define a primeira página a ser carregada, que é a página de lista de produtos.

```csharp
public partial class App : Application
{
    public static SQLiteDataBaseHelper Db { get; } = new SQLiteDataBaseHelper("banco_sqlite_compras.db3");
    public App()
    {
        InitializeComponent();
        MainPage = new NavigationPage(new Views.ListaProduto());
    }
}
```

### 4. **ListaProduto.xaml**

Página principal que exibe a lista de produtos. Ela permite buscar e editar produtos, além de exibir o total acumulado dos itens.

```xml
<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui" ...>
    <!-- Exibição de lista e campos de busca -->
    <ListView ...>
        <ListView.ItemTemplate>
            <DataTemplate>
                <ViewCell>
                    <MenuItem Text="Remover" Clicked="MenuItem_Clicked" />
                    <!-- Dados do produto -->
                </ViewCell>
            </DataTemplate>
        </ListView.ItemTemplate>
    </ListView>
</ContentPage>
```

### 5. **NovoProduto.xaml**

Página para adicionar um novo produto. Contém campos de entrada para descrição, quantidade e preço.

```xml
<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui" ...>
    <VerticalStackLayout>
        <Label Text="Descrição do Produto: " />
        <Entry x:Name="txt_descricao" />
        <!-- Outros campos para quantidade e preço -->
    </VerticalStackLayout>
</ContentPage>
```

### 6. **EditarProduto.xaml**

Página para editar um produto existente. Permite alterar as informações de descrição, quantidade e preço.

```xml
<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui" ...>
    <VerticalStackLayout>
        <Label Text="Descrição do Produto: " />
        <Entry x:Name="txt_descricao" />
        <!-- Outros campos para quantidade e preço -->
    </VerticalStackLayout>
</ContentPage>
```

## Como Rodar o Projeto

### Pré-requisitos

- **.NET 6 ou superior**
- **Visual Studio 2022 ou superior** com suporte para .NET MAUI
- **SQLite** para armazenamento local

### Passos para Execução

1. Clone este repositório:
   ```bash
   git clone https://github.com/SEU_USUARIO/MauiAppMinhasCompras.git
   ```

2. Abra o projeto no Visual Studio.

3. Certifique-se de que a plataforma alvo (Android, iOS, Windows, etc.) esteja configurada corretamente.

4. Execute o projeto no emulador ou dispositivo físico.

## Contribuições

Se você deseja contribuir para este projeto, sinta-se à vontade para enviar um *pull request*. Para qualquer dúvida ou sugestão, crie uma *issue*.

## Licença

Este projeto está licenciado sob a [MIT License](LICENSE).
```

### Explicação do conteúdo do `README.md`:

- **Título**: O nome do aplicativo.
- **Funcionalidades**: Lista das funcionalidades principais do aplicativo, como adicionar, editar, listar, excluir e calcular o total dos produtos.
- **Tecnologias Utilizadas**: Lista das tecnologias usadas no projeto (como .NET MAUI, SQLite e C#).
- **Estrutura do Projeto**: Explicação das principais classes e páginas do aplicativo, com exemplos de código para cada uma delas.
- **Como Rodar o Projeto**: Instruções sobre como configurar o projeto e executá-lo, incluindo os pré-requisitos e os passos necessários.
- **Contribuições**: Como contribuir para o projeto, criando pull requests ou issues.
- **Licença**: Licença do projeto, como MIT.

### Personalização:

- **"SEU_USUARIO"** no link de clone do GitHub deve ser alterado para o seu nome de usuário ou para o nome do repositório.
- **Licença**: A seção de licença pode ser personalizada, conforme a licença que você está utilizando para o projeto (se não for a MIT).
