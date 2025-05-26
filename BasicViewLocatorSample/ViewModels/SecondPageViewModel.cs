using System;
using System.ComponentModel.DataAnnotations;
using ReactiveUI;

namespace BasicViewLocatorSample.ViewModels;

public class SecondPageViewModel : PageViewModelBase
{
    public SecondPageViewModel()
    {
        this.WhenAnyValue(x => x.MailAddress, x => x.Password)
            .Subscribe(_ => UpdateCanNavigateNext());
    }

    private bool _CanNavigateNext;

    public override bool CanNavigateNext
    {
        get => _CanNavigateNext;
        protected set => this.RaiseAndSetIfChanged(ref _CanNavigateNext, value);
    }

    public override bool CanNavigatePrevious
    {
        get => true;
        protected set => throw new NotSupportedException();
    }

    private string? _MailAddress;

    [Required]
    [EmailAddress]
    public string? MailAddress
    {
        get => _MailAddress;
        set => this.RaiseAndSetIfChanged(ref _MailAddress, value);
    }

    private string? _Password;

    [Required]
    public string? Password
    {
        get => _Password;
        set => this.RaiseAndSetIfChanged(ref _Password, value);
    }

    private void UpdateCanNavigateNext()
    {
        CanNavigateNext = !string.IsNullOrEmpty(_MailAddress)
                          && _MailAddress.Contains("@")
                          && !string.IsNullOrEmpty(_Password);
    }
}