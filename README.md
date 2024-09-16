_[![MetaSource.Library Nuget Version](https://img.shields.io/nuget/v/MetaSource.Library?style=flat-square&label=NuGet%3A%20MetaSource.Library)](https://www.nuget.org/packages/MetaSource.Library)_

This is a work in progress. That should allow you to expose types like

* Instance properties
* Methods (not implemented yet)

As typed objects. Primarily through source generation.

## Instance Properties

### About the type

Instance properties inherits from the IInstanceProperty which inherits from INamedMetaItem

```
public interface IInstanceProperty : INamedMetaItem
{
    public Type SourceType { get;  }
    public Type ValueType { get; }

    public object? GetValue(object source);
    public void SetValue(object source, object? value);
    
    public bool CanRead { get; }
    public bool CanWrite { get; }
}

public interface INamedMetaItem
{
    string Key { get; }
    string Name { get; }
}
```

It presents a wrapping of property. So you have access to read and write (if available), have simple meta data like

* Name of property
* Type of containing class
* Key
* A method for getting the value of a property
* A method for setting the value of a property
* Properties to signal if values can be read or set

There is a Generic interface `ITypedInstanceProperty<in TSource,TValue>` that allows for using generic to define the sourcetype and the property return type. 

You can generate these manually if you want. You can also use the `ReflectionGenericInstanceProperty` which will take in a PropertyInfo object and create a relevant IInstanceProperty implementation

Alternatively. And the main purpose of this library is that you can use the source generator that is included in the library

### SourceGenerator

You can decorate a class with the MetaData attribute. The class does not need to be partial. But it can have some advantages. See below 

```csharp
[MetaData]
public partial class Someclass
{
    public string Name {get;set;}
}

```

This will generate a type version of the Name property that will look like 

*NB* Only public properties will have an instance property generated for them

```csharp
public class Someclass_Name_InstanceProperty : GenericInstancePropertyBase<Someclass, string>
{
	public override string Name => "Name";
	public override string Key => "SuperTest.Someclass.Name";

    public override bool CanRead => true;
    public override bool CanWrite => true;
    public override string GetValue(Someclass source)
    {
        return source.Name;
    }
    public override void SetValue(Someclass source, string value)
    {
        source.Name = value;
    }
}

```

It will also generate a static class Someclass_InstanceProperties static class that exposes

* A shortcut to get the given properties for the type
* An extension method to get the Name from the given Source type 
  * For instance someClass.Name_InstanceProperty();
* A method to enumerate all public properties

```csharp
public static class Someclass_InstanceProperties
{
    public static readonly Someclass_Name_InstanceProperty Name = new();
    public static Someclass_Name_InstanceProperty Name_InstanceProperty(this Someclass source)
    {
	    return Name;
    }

    public static System.Collections.Generic.IEnumerable<MetaSource.Library.IInstanceProperty> GetAll()
    {
        yield return Name;      
    }
}

```

If the class decorated with the MetaData attribute is partial additional static properties will be added to the given type

```csharp
public partial class Someclass
{
    public static Someclass_Name_InstanceProperty Name_InstanceProperty => Someclass_InstanceProperties.Name;
}
```

This will present a different way to get an Instance Property 

```csharp
Someclass.Name_InstanceProperty
```