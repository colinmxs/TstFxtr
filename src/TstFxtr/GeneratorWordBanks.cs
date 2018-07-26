using CodenameGenerator;

namespace TstFxtr
{
    public class GeneratorWordBanks
    {
        public static WordBank LoremWordBank = new WordBank(Word.Noun, "Lorems", new WordRepository(new string[] { LoremConstants.Lorem1, LoremConstants.Hairy, LoremConstants.Web2, LoremConstants.Hipster, LoremConstants.Fillerama, LoremConstants.Arrested, LoremConstants.Bacon, LoremConstants.Cupcake, LoremConstants.Beer, LoremConstants.Samuel, LoremConstants.Corporate, LoremConstants.Fillerati, LoremConstants.Zombie, LoremConstants.Gangster, LoremConstants.Journo, LoremConstants.Cat, LoremConstants.Veggie, LoremConstants.Picksum, LoremConstants.Hodor }));
    }
}
