using System.Runtime.InteropServices.Marshalling;

namespace MetaSource.Library;

public static class Extensions
{
    public static T GetPropertyValue<T, TSource>(this TSource source, ITypedInstanceProperty<TSource, T> property)
    {
        return property.GetValue(source);
    }

    public static T GetValue<T, TSource>(this ITypedInstanceProperty<TSource, T> property, TSource source)
    {
        return source.GetPropertyValue(property);
    }

    public static bool CanBeCalledOn(this IInstanceProperty property, object source)
    {
        // check if the given project is the given source type or inherited from it
        return property.SourceType.IsInstanceOfType(source);
    }

    /// <summary>
    /// Safely transfers the value of a property from one object to another if the property can be read and written.
    /// </summary>
    /// <param name="property">The property to be transferred.</param>
    /// <param name="source">The source object from which the property value is read.</param>
    /// <param name="destination">The destination object to which the property value is set.</param>
    /// <returns>True if the property value was successfully transferred; otherwise, false.</returns>
    /// <remarks>The source and destination must be of the same type</remarks>
    public static bool SafeTransfer(this IInstanceProperty property, object source, object destination)
    {
        if (property is not {CanRead: true, CanWrite: true})
            return false;
        
        
            
        var value = property.GetValue(source);
        property.SetValue(destination,value);
        return true;
    }
    
    /// <summary>
    /// Safely transfers the value of a property from one object to another if the property can be read and written.
    /// </summary>
    /// <typeparam name="T">Type of the source and destination objects.</typeparam>
    /// <typeparam name="TValue">Type of the property value.</typeparam>
    /// <param name="property">The property to be transferred.</param>
    /// <param name="source">The source object from which the property value is read.</param>
    /// <param name="destination">The destination object to which the property value is set.</param>
    /// <param name="value">The transferred value of the property.</param>
    /// <returns>True if the property value was successfully transferred; otherwise, false.</returns>
    public static bool SafeTransferTyped<T, TValue>(this ITypedInstanceProperty<T, TValue> property, T source, T destination,
        out TValue value)
    {
        value = default!;

        if (property is {CanRead: true, CanWrite: true})
        {
            var sourceValue = property.GetValue(source);
            property.SetValue(destination, sourceValue);
            value = sourceValue;
            return true;
        }

        return false;
    }

    
    // public static T CloneSimpleProperties<T>(this T source) where T : IInstancePropertyProvider, new()
    // {
    //     var newItem = new T();
    //     foreach (var property in source.GetInstanceProperties().Where(x => x is {CanRead: true, CanWrite: true}))
    //     {
    //         if (property is not ITypedInstanceProperty<T, IInstancePropertyProvider>)
    //         {
    //             
    //         }
    //         var value = property.GetValue(source);
    //         
    //     }
    // }
    //
    
}