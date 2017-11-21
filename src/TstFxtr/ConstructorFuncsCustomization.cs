using System;

namespace TstFxtr
{
    public class ConstructorFuncsCustomization : Customization
    {
        private Func<object>[] @params;

        public ConstructorFuncsCustomization(Type type, Func<object>[] @params) : base(type)
        {
            this.@params = @params;
        }

        internal override object Construct()
        {
            var paramsCount = @params.Length;
            var ctorParams = new object[paramsCount];
            for (int i = 0; i < paramsCount; i++)
            {
                ctorParams[i] = @params[i].Invoke();
            }

            return Activator.CreateInstance(InnerType, ctorParams);
        }
    }
}
