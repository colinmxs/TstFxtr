using System;

namespace TstFxtr
{
    public class GenFxtr
    {
        private static readonly ObjectGenerator _generator;

        static GenFxtr()
        {
            _generator = new ObjectGenerator();            
        }

        public static void Customize(Type type, params object[] @params)
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
