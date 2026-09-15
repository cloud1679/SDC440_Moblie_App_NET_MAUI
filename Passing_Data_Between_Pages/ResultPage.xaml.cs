namespace Passing_Data_Between_Pages;

public partial class ResultPage : ContentPage
{
    public ResultPage(double firstValue, double secondValue, string operation)
    {
        InitializeComponent();

        lblNumbers.Text =
            $"The numbers entered are {firstValue} and {secondValue}.";

        if (operation == "Addition")
        {
            double result = firstValue + secondValue;
            lblResult.Text = $"The sum of the numbers is {result}";
        }
        else if (operation == "Subtraction")
        {
            double result = firstValue - secondValue;
            lblResult.Text = $"The difference of the numbers is {result}";
        }
    }

    private async void OnGoBackClicked(object? sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}