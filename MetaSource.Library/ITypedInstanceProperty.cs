namespace MetaSource.Library;

public interface ITypedInstanceProperty<in TSource,TValue> : IInstanceProperty
{ 
    TValue GetValue(TSource source);
    void SetValue(TSource source, TValue value);
}



