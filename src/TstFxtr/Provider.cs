using System;

namespace TstFxtr
{
    public class Provider : Wrapper
    {
        public Provider(Type type) : base(type)
        {
        }

        public ProvideObjectCustomization Use(object @object)
        {
            return new ProvideObjectCustomization(InnerType, @object);
        }

        public ProvideFuncCustomization Use(Func<object> func)
        {
            return new ProvideFuncCustomization(InnerType, func);
        }
    }
}
