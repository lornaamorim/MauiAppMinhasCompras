using MauiAppMinhasCompras.Helpers;

namespace MauiAppMinhasCompras
{
    public partial class App : Application
    {
        // Instância privada do helper de banco de dados SQLite
        static SQLiteDataBaseHelper _db;

        // Propriedade pública que fornece acesso à instância do banco de dados
        public static SQLiteDataBaseHelper Db
        {
            get
            {
                // Se a instância do banco de dados ainda não foi criada, cria a instância
                if (_db == null)
                {
                    // Caminho do banco de dados SQLite no diretório de dados local do aplicativo
                    string path = Path.Combine(
                        Environment.GetFolderPath(
                            Environment.SpecialFolder.LocalApplicationData),
                        "banco_sqlite_compras.db3");

                    // Criação da instância do helper de banco de dados
                    _db = new SQLiteDataBaseHelper(path);
                }

                // Retorna a instância do banco de dados
                return _db;
            }
        }

        // Construtor da classe App, responsável por inicializar o aplicativo
        public App()
        {
            // Inicializa os componentes do aplicativo (necessário no MAUI)
            InitializeComponent();

            // Define a página principal do aplicativo como uma página de navegação
            // Com a tela inicial sendo a página ListaProduto
            MainPage = new NavigationPage(new Views.ListaProduto());
        }
    }
}



