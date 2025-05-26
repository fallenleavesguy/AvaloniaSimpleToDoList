using System;
using IDataTemplateSample.Models;
using ReactiveUI;

namespace IDataTemplateSample.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private ShapeType _SelectedShapeType;

    public ShapeType SelectedShapeType
    {
        get => _SelectedShapeType;
        set => this.RaiseAndSetIfChanged(ref _SelectedShapeType, value);
    }

    public ShapeType[] AvailableShapeTypes { get; } = Enum.GetValues<ShapeType>();
}