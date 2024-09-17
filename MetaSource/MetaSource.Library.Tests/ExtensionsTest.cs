using System.Linq;
using FluentAssertions;
using JetBrains.Annotations;
using MetaSource.Library;
using MetaSource.Library.SourceGenerator;
using Xunit;

namespace MetaSource.Library.Tests;

[MetaData]
public partial class TestClass
{
    private bool _writeOnly;
    public int? Number { get; set; }
    public string ReadOnly => string.Empty;

    public bool WriteOnly
    {
        set
        {
            _writeOnly = value;
        }
    }
}

[TestSubject(typeof(Extensions))]
public class ExtensionsTest
{

    [Fact]
    public void SafeTransfer_ValidScenario()
    {
        var testClass = new TestClass();
        testClass.Number = 10;
        var destination = new TestClass();
        var success = testClass.Number_InstanceProperty().SafeTransferTyped(testClass,destination, out var result);
        success.Should().BeTrue();
        result.Should().Be(10);
        destination.Number.Should().Be(10);
    }
    
    [Fact]
    public void SafeTransfer_ReadOnly()
    {
        var testClass = new TestClass();
        testClass.Number = 10;
        var destination = new TestClass();
        var success = testClass.ReadOnly_InstanceProperty().SafeTransferTyped(testClass,destination, out var result);
        success.Should().BeFalse();
    }
    
    [Fact]
    public void SafeTransfer_WriteOnly()
    {
        var testClass = new TestClass();
        testClass.Number = 10;
        var destination = new TestClass();
        var success = testClass.WriteOnly_InstanceProperty().SafeTransferTyped(testClass,destination, out var result);
        success.Should().BeFalse();
    }

    [Fact]
    public void SafeTransfer_NonTypeInstanceProperty()
    {
        var testClass = new TestClass();
        testClass.Number = 50;
        var destination  = new TestClass();
        var instanceProperty = testClass.GetInstanceProperties().First();
        instanceProperty.SafeTransfer(testClass, destination);
        
        

        destination.Number.Should().Be(50);
    }
}