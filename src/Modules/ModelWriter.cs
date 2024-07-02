using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TornBattleSimulator.Modules;

public static class ModelWriter
{
    /// <summary>
    ///  Apply the values of <paramref name="source"/> to <paramref name="destination"/>, where <paramref name="destination"/> has null values.
    /// </summary>
    /// <param name="source">The source object to apply from.</param>
    /// <param name="destination">The destination object to apply to.</param>
    /// <returns></returns>
    public static object Apply(object source, object destination)
    {
        foreach (PropertyInfo property in source.GetType().GetProperties())
        {
            object? destinationValue = property.GetValue(destination);
            if (destinationValue == null)
            {
                property.SetValue(destination, property.GetValue(source));
            }
            else if (IsComplex(destinationValue, property))
            {
                property.SetValue(destination, Apply(property.GetValue(source)!, destinationValue));
            }
        }

        return destination;
    }

    private static bool IsComplex(object? value, PropertyInfo property)
    {
        // Objects contain more properties, and are therefore complex - the indiviual
        // properties inside will need to be inspected.
        return Convert.GetTypeCode(value) == TypeCode.Object
            // But collections are objects that are not complex, because we
            // take them as-is.
            && !typeof(IEnumerable).IsAssignableFrom(property.PropertyType);
    }
}