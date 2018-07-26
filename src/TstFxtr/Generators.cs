using CodenameGenerator;

namespace TstFxtr
{
    public class Generators
    {
        public static Generator LoremGenerator
        {
            get
            {
                return new Generator(" ", Casing.LowerCase, GeneratorWordBanks.LoremWordBank);
            }
        }
        
        public static Generator NameGenerator
        {
            get
            {
                var nameGenerator = new Generator(" ", Casing.PascalCase, WordBank.FirstNames, WordBank.LastNames);
                return nameGenerator;
            }
        }

        public static Generator LocationGenerator
        {
            get
            {
                return new Generator(", ", Casing.PascalCase, WordBank.Cities, WordBank.StateNames);
            }
        }
    }
}
