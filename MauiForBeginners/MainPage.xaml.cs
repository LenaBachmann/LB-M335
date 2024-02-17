using MauiForBeginners.ViewModel;

namespace MauiForBeginners;

public partial class MainPage : ContentPage
{

    public MainPage(MainViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
