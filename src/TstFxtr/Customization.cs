using System;

namespace TstFxtr
{
    public abstract class Customization : Wrapper
    {
        public Customization(Type type) : base(type)
        {
        }

        internal abstract object Construct();
    }
}
