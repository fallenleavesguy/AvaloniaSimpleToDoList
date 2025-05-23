namespace CommandSample.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public ReactiveUiCommandsViewModel ReactiveUiCommandsViewModel { get; } = new ReactiveUiCommandsViewModel();
    public CommunityToolkitCommandsViewModel CommunityToolkitCommandsViewModel { get; } = new CommunityToolkitCommandsViewModel();
#pragma warning disable CA1822 // Mark members as static
    public string Greeting => "Welcome to Avalonia!";
#pragma warning restore CA1822 // Mark members as static
}