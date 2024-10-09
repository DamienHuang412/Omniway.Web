using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Practice.Web.BlazorApp.Utilities;

public static class DisplayHelper
{
    public static string GetDisplayName<TModel>(string propertyName)
    {
        var property = typeof(TModel).GetProperty(propertyName);
        if (property == null) return propertyName;
        
        var displayAttribute = property.GetCustomAttribute<DisplayAttribute>();

        return displayAttribute != null && !string.IsNullOrEmpty(displayAttribute.Name)
            ? displayAttribute.Name
            : propertyName;
    }
}