namespace Web_Serice_Authentication_and_Storage_App;

public partial class App : Application
{
    private readonly AppShell shell;
    public App(AppShell shell)
    {
        InitializeComponent();
        this.shell = shell;
    }
    protected override Window CreateWindow(IActivationState? activationState) => new(shell);
}
