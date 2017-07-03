# TstFxtr [![Build status](https://ci.appveyor.com/api/projects/status/ytngg4rgyrnfik9u/branch/master?svg=true)](https://ci.appveyor.com/project/colinmxs/tstfxtr/branch/master)

Simple Object generator for dotnet core. Aims to simplify the creation of domain objects for use in tests.

**Go from this:**
``` c#
//arrange
var thingy = new Thingy
{
    Prop1 = "string",
    Prop2 = 10309,
    Prop3 = "other string"
};
```
**To this:**
``` c#
//arrange
var thingy = Create<Thingy>();
```

## Setup

1. Install TstFxtr from (Nuget)[https://www.nuget.org/packages/TstFxtr]
2. Add a static using clause: `using static TstFxtr.GenFxtr;` to your test class.
3. Call TstFxtr methods.
```c#
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using static TstFxtr.GenFxtr;

namespace TstFxtr.Tests
{
    [TestClass]
    public class GenFxtr
    {
        [TestMethod]
        public void DoesTheJob()
        {
            var customer = Create<Customer>();
            Assert.IsNotNull(customer);
        }
    }
}
```
