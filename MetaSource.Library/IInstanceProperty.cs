namespace MetaSource.Library;

public interface IInstanceProperty : INamedMetaItem
{
    public Type SourceType { get;  }
    public Type ValueType { get; }

    public object? GetValue(object source);
    public void SetValue(object source, object? value);
    
    public bool CanRead { get; }
    public bool CanWrite { get; }
}