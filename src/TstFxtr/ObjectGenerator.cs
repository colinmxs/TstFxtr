using CodenameGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TstFxtr
{
    public class ObjectGenerator
    {
        private readonly Generator _generator;
        private readonly Random _random;
        private List<Customization> _typeCustomizations;
        //private List<Customization> _propertyCustomizations;

        internal ObjectGenerator()
        {
            _generator = new Generator();
            _generator.SetParts(WordBank.Nouns);
            _random = new Random();
            _typeCustomizations = new List<Customization>();
        }

        public void Provide() { }

        public void Customize(Customization customization)
        {
            if (CustomizationExists(customization.InnerType))
            {
                RemoveCustomization(customization.InnerType);
            }
            _typeCustomizations.Add(customization);
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
                        if (prop.SetMethod != null)
                        {                            
                            prop.SetValue(@object, Create(prop.PropertyType));
                        }
                    }
                }
            }
        }
        private bool CustomizationExists(Type type)
        {
            var customizations = _typeCustomizations.Where(c => c.InnerType == type);
            return customizations.Count() > 0;
        }
        private void RemoveCustomization(Type type)
        {
            var customization = _typeCustomizations.Single(c => c.InnerType == type);
            _typeCustomizations.Remove(customization);
        }

        private object Create(Type type)
        {
            object @object;
            if (CustomizationExists(type))
            {
                var customization = _typeCustomizations.Single(c => c.InnerType == type);
                @object = customization.Construct();
                if(customization.GetType() != typeof(ProvideObjectCustomization))
                {
                    FillProperties(type, @object);
                }
            }
            else
            {
                var typeInfo = type.GetTypeInfo();
                if (typeInfo.IsValueType)
                {
                    @object = Activator.CreateInstance(type);
                    if (type == typeof(int))
                    {
                        @object = _random.Next(1, 999);
                    }
                    if (type == typeof(DateTime))
                    {
                        @object = DateTime.UtcNow.AddDays(_random.Next(90));
                    }
                    if (type == typeof(Guid))
                    {
                        @object = Guid.NewGuid();
                    }
                }
                else if (type == typeof(string))
                {
                    @object = _generator.Generate();
                }
                else if(typeInfo.BaseType == typeof(Array))
                {
                    var elementType = type.GetElementType();
                    var array = Array.CreateInstance(elementType, 3);
                    for (int i = 0; i < array.Length; i++)
                    {
                        var parameters = GetParameters(elementType);
                        var arrayObject = Activator.CreateInstance(elementType, parameters);

                        FillProperties(elementType, arrayObject);
                        array.SetValue(arrayObject, i);
                    }
                    foreach (var item in array)
                    {
                        FillProperties(elementType, item);
                    }
                    @object = array;
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
}
