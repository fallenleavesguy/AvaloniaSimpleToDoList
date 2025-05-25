using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reactive.Linq;
using ReactiveUI;

namespace ValidationSample.ViewModels;

public class ValidationUsingINotifyDataErrorInfoViewModel: ViewModelBase, INotifyDataErrorInfo
{
    private Dictionary<string, List<ValidationResult>> errors = new();
    public IEnumerable GetErrors(string? propertyName)
    {
        if (string.IsNullOrEmpty(propertyName))
        {
            return errors.Values.SelectMany(static errors => errors);
        }

        if (this.errors.TryGetValue(propertyName, out var result))
        {
            return result;
        }

        return Array.Empty<ValidationResult>();
    }

    public bool HasErrors => errors.Count > 0;
    public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

    protected void ClearErrors(string? propertyName = null)
    {
        if (string.IsNullOrEmpty(propertyName))
        {
            errors.Clear();
        }
        else
        {
            errors.Remove(propertyName);
        }
        
        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        this.RaisePropertyChanged(nameof(HasErrors));
    }

    protected void AddError(string propertyName, string errorMessage)
    {
        if (!errors.TryGetValue(propertyName, out var propertyErrors))
        {
            propertyErrors = new();
            errors.Add(propertyName, propertyErrors);
        }
        
        propertyErrors.Add(new ValidationResult(errorMessage));
        
        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        this.RaisePropertyChanged(nameof(HasErrors));
    }
    
    private string? _EMail;

    /// <summary>
    /// A property that is validated using INotifyDataErrorInfo
    /// </summary>
    public string? EMail
    {
        get { return _EMail; }
        set { this.RaiseAndSetIfChanged(ref _EMail, value); }
    }
    
    private void Validate_EMail()
    {
        // first of all clear all previous errors
        ClearErrors(nameof(EMail));

        // No empty string allowed
        if (string.IsNullOrEmpty(EMail))
        {
            AddError(nameof(EMail), "This field is required");
        }

        // @-sign required
        if (EMail is null || !EMail.Contains('@'))
        {
            AddError(nameof(EMail), "Don't forget the '@'-sign");
        }
    }

    public ValidationUsingINotifyDataErrorInfoViewModel()
    {
        this.WhenAnyValue(x => x.EMail)
            // .Skip(1)
            .Subscribe(_ => Validate_EMail());
        
        // Initial validation
        // Validate_EMail();
    }
}