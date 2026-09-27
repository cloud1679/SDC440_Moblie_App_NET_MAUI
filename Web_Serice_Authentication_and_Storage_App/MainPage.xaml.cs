using Web_Serice_Authentication_and_Storage_App.ViewModels;

namespace Web_Serice_Authentication_and_Storage_App;

public partial class MainPage : ContentPage
{
    public MainPage(LoginViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
