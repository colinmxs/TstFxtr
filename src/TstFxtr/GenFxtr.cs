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
        
        public static Customizer Customize(Type type)
        {
            return new Customizer(type);
        }
        
        public static Provider Provide(Type type)
        {
            return new Provider(type);
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
