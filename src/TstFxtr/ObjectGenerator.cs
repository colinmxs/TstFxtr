using CodenameGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TstFxtr
{
    class ObjectGenerator
    {
        private readonly Generator _generator;
        private readonly Random _random;
        private List<Customization> _customizations;

        internal ObjectGenerator()
        {
            _generator = new Generator();
            _generator.SetParts(WordBank.Nouns);
            _random = new Random();
            _customizations = new List<Customization>();
        }

        internal Customization Customize(Type type)
        {
            var customization = new Customization(type);
            _customizations.Add(customization);
            return customization;
        }

        internal TEntity Create<TEntity>()
        {
            var type = typeof(TEntity);
            var @object = (TEntity)Create(type);
            return @object;
        }

        private void FillProperties(Type type, object @object)
        {
            var publicProperties = type.GetProperties();
            foreach (var prop in publicProperties)
            {
                if (prop.PropertyType != typeof(string) && prop.PropertyType.GetTypeInfo().GetInterface("IEnumerable") != null)
                { }
                else
                {
                    var value = prop.GetValue(@object);
                    if (prop.PropertyType.IsDefaultValue(value))
                    {
                        prop.SetValue(@object, Create(prop.PropertyType));
                    }
                }
            }
        }

        private object Create(Type type)
        {
            var customizationExists = _customizations.Any(c => c.InnerType == type);
            object @object;
            if (customizationExists)
            {
                @object = _customizations.Single(c => c.InnerType == type).Construct();
                FillProperties(type, @object);
            }
            else
            {
                var typeInfo = type.GetTypeInfo();
                if (typeInfo.IsValueType)
                {
                    @object = Activator.CreateInstance(type);
                    if (type == typeof(int))
                    {
                        @object = _random.Next(1, 999999999);
                    }
                    if (type == typeof(DateTime))
                    {
                        @object = DateTime.UtcNow.AddDays(_random.Next(90));
                    }
                }
                else if (type == typeof(string))
                {
                    @object = _generator.Generate();
                }
                else
                {
                    var parameters = GetParameters(type);
                    @object = Activator.CreateInstance(type, parameters);
                    FillProperties(type, @object);
                }
            }
            return @object;
        }

        private object[] GetParameters(Type type)
        {
            var simplestCtor = type.GetSimplestCtor();

            var parameters = new object[simplestCtor.GetParameters().Length];

            for (int i = 0; i < simplestCtor.GetParameters().Length; i++)
            {
                var param = simplestCtor.GetParameters()[i];
                var paramType = param.ParameterType;
                parameters[i] = Create(paramType);
            }

            return parameters;
        }
    }

    internal class Customization
    {
        internal readonly Type InnerType;
        object[] constructorParams;

        internal Customization(Type type)
        {
            InnerType = type;
        }

        internal void ConstructorParams(params object[] @params)
        {
            constructorParams = @params;
        }

        internal object Construct()
        {
            object @object = Activator.CreateInstance(InnerType, constructorParams);

            return @object;
        }
    }
}
