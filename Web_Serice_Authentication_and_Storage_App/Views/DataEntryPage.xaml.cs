using Web_Serice_Authentication_and_Storage_App.ViewModels;

namespace Web_Serice_Authentication_and_Storage_App.Views;

public partial class DataEntryPage : ContentPage
{
    private readonly DataEntryViewModel viewModel;
    public DataEntryPage(DataEntryViewModel viewModel)
    {
        InitializeComponent();
        this.viewModel = viewModel;
        BindingContext = viewModel;
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await viewModel.LoadAsync();
    }
}
