using System;

namespace TstFxtr
{
    public class ProvideFuncCustomization : Customization
    {
        private Func<object> func;

        public ProvideFuncCustomization(Type type, Func<object> func) : base(type)
        {
            this.func = func;
        }

        internal override object Construct()
        {
            return func();
        }
    }
}
