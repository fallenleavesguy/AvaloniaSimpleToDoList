using System.Windows.Input;
using DynamicData;
using ReactiveUI;

namespace BasicViewLocatorSample.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public MainWindowViewModel()
    {
        _CurrentPage = Pages[0];

        var canNavNext = this.WhenAnyValue(x => x.CurrentPage.CanNavigateNext);
        var canNavPrevious = this.WhenAnyValue(x => x.CurrentPage.CanNavigatePrevious);

        NavigateNextCommand = ReactiveCommand.Create(NavigateNext, canNavNext);
        NavigatePreviousCommand = ReactiveCommand.Create(NavigatePrevious, canNavPrevious);
    }

    private readonly PageViewModelBase[] Pages =
    {
        new FirstPageViewModel(),
        new SecondPageViewModel(),
        new ThirdPageViewModel()
    };

    private PageViewModelBase _CurrentPage;

    public PageViewModelBase CurrentPage
    {
        get => _CurrentPage;
        private set => this.RaiseAndSetIfChanged(ref _CurrentPage, value);
    }
    
    public ICommand NavigateNextCommand { get; }

    private void NavigateNext()
    {
        var index = Pages.IndexOf(CurrentPage) + 1;
        
        CurrentPage = Pages[index];
    }
    
    public ICommand NavigatePreviousCommand { get; }

    private void NavigatePrevious()
    {
        var index = Pages.IndexOf(CurrentPage) - 1;
        CurrentPage = Pages[index];
    }
}