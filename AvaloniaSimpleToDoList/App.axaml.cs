using System.Linq;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using AvaloniaSimpleToDoList.ViewModels;
using AvaloniaSimpleToDoList.Views;

namespace AvaloniaSimpleToDoList;

public partial class App : Application
{
    private readonly MainWindowViewModel _mainWindowViewModel = new MainWindowViewModel();
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override async void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // Line below is needed to remove Avalonia data validation.
            // Without this line you will get duplicate validations from both Avalonia and CT
            BindingPlugins.DataValidators.RemoveAt(0);
            desktop.MainWindow = new MainWindow
            {
                DataContext = _mainWindowViewModel,
            };
            desktop.ShutdownRequested += DesktopOnShutdownRequested;
        }

        base.OnFrameworkInitializationCompleted();
        await InitMainViewModelAsync();
    }

    private bool _canClose = false;

    private async void DesktopOnShutdownRequested(object? sender, ShutdownRequestedEventArgs e)
    {
        e.Cancel = !_canClose;

        if (!_canClose)
        {
            var itemsToSave = _mainWindowViewModel.ToDoItems.Select(item => item.GetToDoItem());
            await ToDoListFileService.SaveToFileAsync(itemsToSave);

            _canClose = true;
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.Shutdown();
            }
        }
    } 

    private async Task InitMainViewModelAsync()
    {
        var itemsLoaded = await ToDoListFileService.LoadFromFileAsync();

        if (itemsLoaded != null)
        {
            foreach (var item in itemsLoaded)
            {
                _mainWindowViewModel.ToDoItems.Add(new ToDoItemViewModel(item));
            }
        }
    }
}