namespace ValidationSample.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public ValidationUsingDataAnnotationViewModel ValidationUsingDataAnnotationViewModel { get; } = new();

    public ValidationUsingINotifyDataErrorInfoViewModel ValidationUsingINotifyDataErrorInfoViewModel { get; }
        = new();
}