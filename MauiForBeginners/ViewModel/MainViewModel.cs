using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiForBeginners.models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http.Json;

namespace MauiForBeginners.ViewModel;

public partial class MainViewModel : ObservableObject
{
    IConnectivity connectivity;

    public MainViewModel(IConnectivity con)
    {
        connectivity = con;

        Items = new ObservableCollection<string>();
        Quote = "Be Happy - Unknown";

        if(App.QuoteRepo == null)
        {
            Quotes = new ObservableCollection<SavedQuote>();
        }
        else
        {
            Quotes= new ObservableCollection<SavedQuote>(App.QuoteRepo.GetAllQuotes());
        }
    }

    [ObservableProperty]
    float xDir;
    [ObservableProperty]
    float yDir;
    [ObservableProperty]
    ObservableCollection<SavedQuote> quotes;

    [ObservableProperty]
    ObservableCollection<string> items;

    private string _quote;

    public string Quote
    {
        get
        {
            return _quote;
        }
        set
        {
            if( _quote != value )
            {
                _quote = value;
                OnPropertyChanged();
            }
        }
    }


    [RelayCommand]
    void Save(string s)
    {
        if (App.QuoteRepo != null)
        {
            App.QuoteRepo.AddNewQuote(Quote);
            Quotes = new ObservableCollection<SavedQuote>(App.QuoteRepo.GetAllQuotes());
        }
        else
        {
            Quotes.Add(new SavedQuote { Quote = Quote });
        }
        Quote = string.Empty;
    }

    [RelayCommand]
    async void Get()
    {
        HttpClient httpClient = new HttpClient();

        var url = "https://zenquotes.io/api/random";
        var response = await httpClient.GetAsync(url);

        List<ApiQuote> quotes = null;

        if (response.IsSuccessStatusCode)
        {
            quotes = await response.Content.ReadFromJsonAsync<List<ApiQuote>>();
        }

        string text = quotes[0].q; // Quote text
        string author = quotes[0].a; // Author

        Quote = text + " - " + author;
        Console.WriteLine(Quote);
    }
}
