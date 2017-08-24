using System;

namespace TstFxtr
{
    public class ProvideObjectCustomization : Customization
    {
        private object @object;

        public ProvideObjectCustomization(Type type, object @object) : base(type)
        {
            this.@object = @object;
        }

        internal override object Construct()
        {
            return @object;
        }
    }
}
