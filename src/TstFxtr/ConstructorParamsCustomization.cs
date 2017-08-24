using System;

namespace TstFxtr
{
    public class ConstructorParamsCustomization : Customization
    {
        object[] constructorParams;

        public ConstructorParamsCustomization(Type type, params object[] @params) : base(type)
        {
            constructorParams = @params;
        }

        internal override object Construct()
        {
            object @object = Activator.CreateInstance(InnerType, constructorParams);
            return @object;
        }
    }
}
