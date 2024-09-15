using System.Reflection;

namespace MetaSource.Library;

public class ReflectionGenericInstanceProperty : IInstanceProperty
{
    private readonly PropertyInfo _propertyInfo;

    public ReflectionGenericInstanceProperty(PropertyInfo propertyInfo)
    {
        _propertyInfo = propertyInfo;
        Name = propertyInfo.Name;
        SourceType = propertyInfo.DeclaringType ??
                     throw new InvalidOperationException("The passed in property must belong to a type");
        ValueType = propertyInfo.PropertyType;
        CanRead = propertyInfo.CanRead;
        CanWrite = propertyInfo.CanWrite;
    }

    public string Name { get; }
    public Type SourceType { get; }
    public Type ValueType { get; }
    
    public object? GetValue(object source)
    {
        if (!CanRead)
            throw new InvalidOperationException("This property does not suppport read");
        
        return _propertyInfo.GetMethod!.Invoke(source, null);
    }

    public void SetValue(object source, object? value)
    {
        if (!CanWrite)
            throw new InvalidOperationException("This property does not support write operations");
        
        _propertyInfo.SetMethod!.Invoke(source, [value]);
    }

    public bool CanRead { get; }
    public bool CanWrite { get; }
}