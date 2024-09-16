// This code will not compile until you build the project with the Source Generators

using MetaSource.Library.SourceGenerator;

namespace SuperTest;

[MetaData]
public partial class Someclass
{
    public string Name { get; set; }
    
    public int Age { get; }
}

public class Processor()
{
    public void Method()
    {
        var someclassAgeInstanceProperty = new Someclass_Age_InstanceProperty();
        var test = new Someclass();
        someclassAgeInstanceProperty.GetValue(test);
    }
}