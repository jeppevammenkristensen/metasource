namespace MetaSource.Library;

public static class Extensions
{
    public static T GetPropertyValue<T, TSource>(this TSource source, GenericInstancePropertyBase<TSource, T> property)
    {
        return property.GetValue(source);
    }

    public static T GetValue<T, TSource>(this GenericInstancePropertyBase<TSource, T> property, TSource source)
    {
        return source.GetPropertyValue(property);
    }
}