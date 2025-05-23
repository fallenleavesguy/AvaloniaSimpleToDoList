using System;
using ReactiveUI;

namespace BasicMvvmSample.ViewModels;

public class ReactiveViewModel: ReactiveObject
{
   public ReactiveViewModel()
   {
      // We can listen to any property changes with "WhenAnyValue" and do whatever we want in "Subscribe".
      this.WhenAnyValue(o => o.Name)
         .Subscribe(new Action<object>(_ => this.RaisePropertyChanged(nameof(Greeting))));
   }
   private string? _Name;

   public string? Name
   {
      get
      {
         return _Name;
      }
      set
      {
         this.RaiseAndSetIfChanged(ref _Name, value);
      }
   }
   
   // Greeting will change based on a Name.
   public string Greeting
   {
      get
      {
         if (string.IsNullOrEmpty(Name))
         {
            // If no Name is provided, use a default Greeting
            return "Hello World from Avalonia.Samples";
         }
         else
         {
            // else greet the User.
            return $"Hello {Name}";
         }
      }
   }
}