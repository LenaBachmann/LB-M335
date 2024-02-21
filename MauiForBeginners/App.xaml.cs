namespace MauiForBeginners
{
    public partial class App : Application
    {
        public static QuoteRepository QuoteRepo { get; private set; }
        public App(QuoteRepository repo)
        {
            InitializeComponent();

            MainPage = new AppShell();

            QuoteRepo = repo;
        }
    }
}
