using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Metadata;
using IDataTemplateSample.Models;

namespace IDataTemplateSample.DataTemplates;

public class ShapesTemplateSelector: IDataTemplate
{
    [Content]
    public Dictionary<string, IDataTemplate> AvailableTemplates { get; } = new();
    public Control? Build(object? param)
    {
        var key = param?.ToString();
        if (key is null)
        {
            throw new ArgumentNullException(nameof(param));
        }

        return AvailableTemplates[key].Build(param);
    }

    public bool Match(object? data)
    {
        var key = data?.ToString();

        return data is ShapeType
               && !string.IsNullOrEmpty(key)
               && AvailableTemplates.ContainsKey(key);
    }
}