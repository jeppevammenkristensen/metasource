using System.Collections.ObjectModel;
using System.Reflection;

namespace MetaSource.Library;

public abstract class ReflectionBasedInstancePropertyProvider : IInstancePropertyProvider
{
    private Lazy<ReadOnlyCollection<IInstanceProperty>> _lazyProperties;

    public ReflectionBasedInstancePropertyProvider()
    {
        _lazyProperties = new Lazy<ReadOnlyCollection<IInstanceProperty>>(DoGetInstanceProperties);
    }
    
    private static readonly Dictionary<Type, ReadOnlyCollection<IInstanceProperty>> InstancePropertyMappings = new Dictionary<Type, ReadOnlyCollection<IInstanceProperty>>();

    private ReadOnlyCollection<IInstanceProperty> DoGetInstanceProperties()
    {
        if (InstancePropertyMappings.TryGetValue(GetType(), out var properties))
        {
            return properties;
        }
        
        var type = this.GetType();
        // get public properties that can be called from an instance of the given type
        var propertyInfos = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        
        //var propertyInfos = type.GetProperties();
        var instance = new ReadOnlyCollection<IInstanceProperty>(propertyInfos.Select(x => new ReflectionGenericInstanceProperty(x)).OfType<IInstanceProperty>().ToList());
        InstancePropertyMappings.Add(type, instance);
        return instance;

    }

    public IEnumerable<IInstanceProperty> GetInstanceProperties()
    {
        return _lazyProperties.Value;
    }
}