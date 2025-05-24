using ReactiveUI;
using ValueConversionSample.Converter;

namespace ValueConversionSample.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    // The initial value is 2. 
    private decimal? _Number1 = 2;

    /// <summary>
    /// This is our Number 1
    /// </summary>
    public decimal? Number1
    {
        get { return _Number1; }
        set { this.RaiseAndSetIfChanged(ref _Number1, value); }
    }

#pragma warning disable CA1822 // Mark members as static
    public string Greeting => "Welcome to Avalonia!";
#pragma warning restore CA1822 // Mark members as static
}