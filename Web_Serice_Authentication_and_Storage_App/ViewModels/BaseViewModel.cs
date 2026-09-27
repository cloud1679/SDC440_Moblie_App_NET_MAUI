using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Web_Serice_Authentication_and_Storage_App.ViewModels;

public abstract class BaseViewModel : INotifyPropertyChanged
{
    public string StudentName => "Richard Burns";
    private string message = "";
    public string Message { get => message; set => SetProperty(ref message, value); }
    private bool isBusy;
    public bool IsBusy
    {
        get => isBusy;
        protected set
        {
            if (SetProperty(ref isBusy, value)) OnPropertyChanged(nameof(IsNotBusy));
        }
    }
    public bool IsNotBusy => !IsBusy;
    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? name = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string? name = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(name);
        return true;
    }
}
