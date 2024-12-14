using Microsoft.IdentityModel.Tokens;
using System.Reflection;

namespace EmployeeManagement.Extensions;

public static class ClassExtensions
{
    public static Dictionary<string, object?> GetPropertyValues(object from, string[]? excluding = null)
    {
        ArgumentNullException.ThrowIfNull(from);

        Type type = from.GetType();
        PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        Dictionary<string, object?> propertyValues = [];

        if (excluding is not null && excluding.Length != 0)
        {
            properties = properties.Where(x => !excluding.Contains(x.Name)).ToArray();
        }

        foreach (PropertyInfo property in properties)
        {
            var propertyName = property.Name;
            var propertyValue = property.GetValue(from);
            propertyValues[propertyName] = propertyValue; // null value assigning is OK
        }

        return propertyValues;
    }
    public static void AssignProperties(object from, object to)
    {
        // Get the type of the source and destination objects
        Type sourceType = from.GetType();
        Type destinationType = to.GetType();

        // Get all public properties of the source and destination types
        PropertyInfo[] sourceProperties = sourceType.GetProperties();

        // Iterate through source properties and assign values to destination properties
        foreach (PropertyInfo sourceProperty in sourceProperties)
        {
            if (!sourceProperty.Name.IsNullOrEmpty())
            {
                // Find a matching property in the destination type
                PropertyInfo? destinationProperty = destinationType.GetProperty(sourceProperty.Name);

                // Check if the property exists in the destination type and is writable
                if (destinationProperty != null && destinationProperty.CanWrite)
                {
                    // Get the value of the source property
                    object? value = sourceProperty.GetValue(from);

                    if (value is not null)
                    {
                        // Set the value of the destination property
                        destinationProperty.SetValue(to, value);
                    }
                }
            }
        }
    }

    public static void AssignProperties(Dictionary<string, object?> from, object to)
    {
        // Get the type of the source and destination objects            
        Type destinationType = to.GetType();

        // Iterate through source properties and assign values to destination properties
        foreach (var sourceProperty in from)
        {
            // Find a matching property in the destination type
            PropertyInfo? destinationProperty = destinationType.GetProperty(sourceProperty.Key);

            // Check if the property exists in the destination type and is writable
            if (destinationProperty != null && destinationProperty.CanWrite)
            {
                // Get the value of the source property
                object? value = sourceProperty.Value;

                // Set the value of the destination property
                destinationProperty.SetValue(to, value);
            }
        }
    }
    public static object MapProperties(object from, object to, string[]? excluding = null)
    {
        var valuesToAdd = GetPropertyValues(from: from, excluding: excluding);
        AssignProperties(from: valuesToAdd, to: to);
        return to;
    }

}
