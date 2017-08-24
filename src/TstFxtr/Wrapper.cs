using System;

namespace TstFxtr
{
    public class Wrapper
    {
        internal readonly Type InnerType;

        protected Wrapper(Type type)
        {
            InnerType = type;
        }
    }
}
