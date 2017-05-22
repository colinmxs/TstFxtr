# TstFxtr

Built to help simplify the "Arrange" step in test suites until Autofixture supports dotnet core.


**Go from this:**
``` c#
[TestMethod]
public void Test1()
{
    //arrange
    var thingy = new Thingy
    {
        Prop1 = "string",
        Prop2 = 10309,
        Prop3 = "other string"
    };

    //act
    var result = thingy.DoSomething();

    //assert
    Assert.Something(result);
}
```
**To this:**
``` c#
[TestMethod]
public void Test1()
{
    //arrange
    var thingy = Create<Thingy>();

    //act
    var result = thingy.DoSomething();

    //assert
    Assert.Something(result);
}
```

Simply add a static using clause: `using static TstFxtr.GenFxtr;`
Works best for well-designed domain classes leveraging ctor dependency injection.
