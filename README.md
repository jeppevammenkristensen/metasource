This is a work in progress. That should allow you to expose types like

* Instance properties
* Methods

As typed objects. Primarily through source generation.

Current POC 

```csharp
[MetaData]
public class Someclass
{
    public string Name {get;set;}
}

```

Will generate a typed class
