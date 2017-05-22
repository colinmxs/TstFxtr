using System;
using System.Reflection;

namespace TstFxtr
{
    internal static class TypeExtensions
    {
        internal static ConstructorInfo GetSimplestCtor(this Type type)
        {
            var ctors = type.GetConstructors();
            if (ctors.Length == 0)
            {
                throw new NotSupportedException(string.Format("The type {0} has no public constructor.", type.FullName));
            }
            ConstructorInfo simplestCtor = ctors[0];
            foreach (var ctor in ctors)
            {
                if (ctor.GetParameters().Length < simplestCtor.GetParameters().Length)
                {
                    simplestCtor = ctor;
                }
            }
            return simplestCtor;
        }

        internal static bool IsDefaultValue(this Type type, object @object)
        {
            if (!type.GetTypeInfo().IsValueType)
            {
                if (@object == null)
                {
                    return true;
                }
                return false;
            }
            var defaultValue = Activator.CreateInstance(type);
            var @bool = false;
            if (defaultValue == null)
            {
                @bool = @object == null;
            }
            else
            {
                @bool = @object.Equals(defaultValue);
            }
            return @bool;
        }
    }
}
