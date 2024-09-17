namespace MetaSource.Library;

public interface ITypedInstanceProperty<in TSource,TValue> : IInstanceProperty
{ 
    TValue GetValue(TSource source);
    void SetValue(TSource source, TValue value);
}

public class Test
{
    public int Get { get; }
    
    
}   



public abstract class GenericInstancePropertyBase<TSource, TValue> : ITypedInstanceProperty<TSource,TValue>
{
    public abstract string Name { get; }
    public Type SourceType => typeof(TSource);
    
    public abstract string Key { get; }

    public Type ValueType => typeof(TValue);

    public abstract TValue GetValue(TSource source);

    object? IInstanceProperty.GetValue(object source)
    {
        if (source is TSource typedSource)
        {
            return GetValue(typedSource);
        }
        else
        {
            throw new InvalidOperationException($"Source was not of type {typeof(TSource)} but was {source.GetType()}");    
        }
        
        
    }

    public abstract void SetValue(TSource source, TValue value);

    void IInstanceProperty.SetValue(object source, object? value)
    {   
        if (source is TSource typedSource && value is TValue typedValue)
        {
            SetValue(typedSource, typedValue);
        }

        else
        {
            // throw good exception to user
            throw new InvalidOperationException($"Source was not of type {typeof(TSource)} and value {typeof(TValue)} but was {source.GetType()} and {value?.GetType()}");    
        }
        
    }

    public abstract bool CanRead { get; }
    public abstract bool CanWrite { get; }
}