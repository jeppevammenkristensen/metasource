using FluentAssertions;
using Xunit;
using Xunit.Sdk;

namespace MetaSource.Library.Tests;


public class GenericInstanceTest
{
    [Fact]
    public void TestGenericInstanceProperty()
    {
        var instance = new InstanceTest();
        var property = new InstanceTestProperty();
        property.SetValue(instance,42);
        property.GetValue(instance).Should().Be(42);
    }
}

public class InstanceTest
{
    public int SomeProperty { get; set; }
}

public class InstanceTestProperty : GenericInstancePropertyBase<InstanceTest, int>
{
    public override string Name => nameof(InstanceTest.SomeProperty);
    public override string Key => "SomeKey";
    public override int GetValue(InstanceTest source)
    {
        return source.SomeProperty;
    }

    public override void SetValue(InstanceTest source, int value)
    {
        source.SomeProperty = value;
    }

    public override bool CanRead => true;
    public override bool CanWrite => true;
}