using System;
using System.Linq;
using FluentAssertions;
using JetBrains.Annotations;
using MetaSource.Library;
using Xunit;

namespace MetaSource.Library.Tests;

[TestSubject(typeof(ReflectionGenericInstanceProperty))]
public class ReflectionGenericInstancePropertyTest
{

    [Fact]
    public void MapsReadWriteProperty_ValidValues_AndCanGetAndSet()
    {
        var result = typeof(TestClass)
            .GetProperties().Select(x => new ReflectionGenericInstanceProperty(x)).ToList();

        var readWriteProperty = result[0];
        readWriteProperty.CanRead.Should().BeTrue();
        readWriteProperty.CanWrite.Should().BeTrue();

        readWriteProperty.SourceType.Should().Be<TestClass>();
        readWriteProperty.ValueType.Should().Be<string>();
        
        var testClass = new TestClass();
        
        readWriteProperty.SetValue(testClass,"Expected");
        readWriteProperty.GetValue(testClass).Should().Be("Expected");
        
        testClass.ReadWriteProperty.Should().Be("Expected");
    }

    [Fact]
    public void MapsReadProperty_ValidValues_AndCanSet()
    {
        var result = typeof(TestClass)
            .GetProperties().Select(x => new ReflectionGenericInstanceProperty(x)).ToList();

        var readProperty = result[1];
        readProperty.CanRead.Should().BeTrue();
        readProperty.CanWrite.Should().BeFalse();

        var testClass = new TestClass();
        readProperty.GetValue(testClass).Should().Be(testClass.ReadonlyProperty);

        Assert.Throws<InvalidOperationException>(() => readProperty.SetValue(testClass, "Not allowed"));
    }
    
    public class TestClass
    {
        private string _writeProperty;
        public string ReadWriteProperty { get; set; }

        public string ReadonlyProperty { get; } = "ReadonlyProperty";

        public string WriteProperty
        {
            set => _writeProperty = value;
        }
    
        public string GetWriteProperty()
        {
            return _writeProperty;
        }
    }
}

