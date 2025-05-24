using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using Avalonia.Data;
using Avalonia.Data.Converters;

namespace ValueConversionSample.Converter;

public class MathMultiConverter: IMultiValueConverter
{
    public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (values.Count != 3)
        {
            Trace.WriteLine("Exactly 3 values are expected for MathMultiConverter");
            return BindingOperations.DoNothing;
        }

        
        string operation = values[0] as string ?? "+";

        decimal value1 = values[1] as decimal? ?? 0;
        decimal value2 = values[2] as decimal? ?? 0;
    
        switch (operation)
        {
            case "+":
                return value1 + value2;
            case "-":
                return value1 - value2;
            case "*":
                return value1 * value2;
            case "/":
                if (value2 == 0)
                {
                    return new BindingNotification(new DivideByZeroException("Don't do this"), BindingErrorType.Error);
                }
                return value1 / value2;
        }
        
        return new BindingNotification(new InvalidOperationException("Something went wrong with the MathMultiConverter"), BindingErrorType.Error);
    }
}