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

    private decimal? _Number2 = 3;

    public decimal? Number2
    {
        get => _Number2;
        set => this.RaiseAndSetIfChanged(ref _Number2, value);
    }
    
    private string _Operator = "+";

    public string Operator
    {
        get => _Operator;
        set => this.RaiseAndSetIfChanged(ref _Operator, value);
    }

    public string[] AvailableMathOperators { get; } = new string[]
    {
        "+", "-", "*", "/"
    };
}