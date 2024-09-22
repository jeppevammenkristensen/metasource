namespace MetaSource.Library;

public interface IInstancePropertyProvider
{
    public IEnumerable<IInstanceProperty> GetInstanceProperties();
}