// This code will not compile until you build the project with the Source Generators

using System.IO.Compression;
using MetaSource.Library;
using MetaSource.Library.SourceGenerator;

namespace SuperTest;

[MetaData]
public partial class Someclass
{
    public string Name { get; set; }
    
    public int Age { get; }
}

public class TransferSample()
{
    public void Transfer()
    {
        var source = new Someclass();
        var destination = new Someclass();
        
        foreach (var instanceProperty in source.GetInstanceProperties())
        {
            instanceProperty.SafeTransfer(source, destination);
        }
    }
}