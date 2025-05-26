using System;

namespace BasicViewLocatorSample.ViewModels;

public class ThirdPageViewModel: PageViewModelBase
{
    public string Message => "Done";

    public override bool CanNavigateNext
    {
        get => false;
        protected set => throw new NotSupportedException();
    }

    public override bool CanNavigatePrevious { get => true;
        protected set => throw new NotSupportedException();
    }
}