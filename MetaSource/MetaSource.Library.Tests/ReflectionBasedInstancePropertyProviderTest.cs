using FluentAssertions;
using JetBrains.Annotations;
using MetaSource.Library;
using Xunit;

namespace MetaSource.Library.Tests;

[TestSubject(typeof(ReflectionBasedInstancePropertyProvider))]
public class ReflectionBasedInstancePropertyProviderTest
{

    [Fact]
    public void ReflectionBasedInstancePropertyProvider_GetProperty_ShouldReturnPropert()
    {
        var reflectionBased = new ReflectionBased();
        reflectionBased.GetInstanceProperties().Should().HaveCount(3);
    }
}

public class ReflectionBased : ReflectionBasedInstancePropertyProvider
{
    private string _writeOnly;
    public string Name { get; set; }
    public int OtherName { get; }

    public string WriteOnly
    {
        set => _writeOnly = value;
    }
}