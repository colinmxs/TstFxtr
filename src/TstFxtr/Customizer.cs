using System;

namespace TstFxtr
{
    public class Customizer : Wrapper
    {
        public Customizer(Type type) : base(type)
        {
        }

        public ConstructorParamsCustomization ConstructorParams(params object[] @params)
        {
            return new ConstructorParamsCustomization(InnerType, @params);
        }
        public ConstructorFuncsCustomization ConstructorFuncs(params Func<object>[] @params)
        {
            return new ConstructorFuncsCustomization(InnerType, @params);
        }
    }
}
