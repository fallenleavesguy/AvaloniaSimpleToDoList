using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Shapes;
using Avalonia.Data;

namespace RatingControlSample.Controls;

[TemplatePart("PART_StarsPresenter", typeof(ItemsControl))]
public class RatingControl : TemplatedControl
{
    public RatingControl()
    {
        UpdateStars();
    }
    public static readonly StyledProperty<int> NumberOfStarsProperty =
        AvaloniaProperty.Register<RatingControl, int>(
            nameof(NumberOfStars),
            defaultValue: 5,
            coerce: CoerceNumberOfStars);

    public static readonly DirectProperty<RatingControl, int> ValueProperty =
        AvaloniaProperty.RegisterDirect<RatingControl, int>(
            nameof(Value),
            o => o.Value,
            ((control, v) => control.Value = v),
            defaultBindingMode: BindingMode.TwoWay,
            enableDataValidation: true);

    public static readonly DirectProperty<RatingControl, IEnumerable<int>> StarsProperty = 
            AvaloniaProperty.RegisterDirect<RatingControl, IEnumerable<int>>(
                nameof(Stars),
                o => o.Stars);

    private IEnumerable<int> _stars = Enumerable.Range(1, 5);

    public IEnumerable<int> Stars
    {
        get => _stars;
        private set => SetAndRaise(StarsProperty, ref _stars, value);
    }
    public int NumberOfStars
    {
        get => GetValue(NumberOfStarsProperty);
        set => SetValue(NumberOfStarsProperty, value);
    }

    private static int CoerceNumberOfStars(AvaloniaObject instance, int value)
    {
        return value < 1 ? 1 : value;
    }

    private int _value;

    public int Value
    {
        get => _value;
        set => SetAndRaise(ValueProperty, ref _value, value);
    }

    protected override void UpdateDataValidation(AvaloniaProperty property, BindingValueType state, Exception? error)
    {
        base.UpdateDataValidation(property, state, error);

        if (property == ValueProperty)
        {
            DataValidationErrors.SetError(this, error);
        }
    }

    private void UpdateStars()
    {
        Stars = Enumerable.Range(1, NumberOfStars);
    }

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);
        if (change.Property == NumberOfStarsProperty)
        {
            UpdateStars();
        }
    }
    private ItemsControl? _starsPresenter;
    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);

        if (_starsPresenter is not null)
        {
            _starsPresenter.PointerReleased -= StarsPresenter_PointerReleased;
        }
        _starsPresenter = e.NameScope.Find("PART_StarsPresenter") as ItemsControl;

        if (_starsPresenter != null)
        {
            _starsPresenter.PointerReleased += StarsPresenter_PointerReleased;
        }
    }
    private void StarsPresenter_PointerReleased(object? sender, Avalonia.Input.PointerReleasedEventArgs e)
    {
        if (e.Source is Path star)
        {
            Value = star.DataContext as int? ?? 0;
        }
    }
}