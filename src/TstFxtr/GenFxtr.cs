using System;

namespace TstFxtr
{
    public class GenFxtr
    {
        public static readonly ObjectGenerator _generator;

        static GenFxtr()
        {
            _generator = new ObjectGenerator();            
        }

        /// <summary>
        /// Specify the objects that should be passed as the constructor parameters when an object of the provided Type is created.
        /// </summary>
        /// <param name="type">The Type to customize</param>
        /// <param name="params">The params to pass into the ctor</param>
        public static void Customize(Type type, params object[] @params)
        {
            _generator.Customize(type)
                .ConstructorParams(@params);
        }
        
        public static void Customize(Type type, params Func<object>[] @params)
        {
            _generator.Customize(type)
                .ConstructorParams(@params);
        }

        public static TEntity Create<TEntity>()
        {
            return _generator.Create<TEntity>();
        }

        public static TEntity[] CreateMany<TEntity>(int count = 0)
        {
            if (count == 0)
                count = 3;

            TEntity[] arr = new TEntity[count];
            for (int i = 0; i < count; i++)
            {
                arr[i] = Create<TEntity>();
            }

            return arr;
        }
    }    
}
