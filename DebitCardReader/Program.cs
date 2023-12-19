using DebitCardReader.models;

namespace DebitCardReader
{
    public class Program
    {
        private static List<DebitCard> cards = new List<DebitCard>();
        static void Main(string[] args)
        {
            foreach (string card in File.ReadAllLines("input.txt").Skip(1))
            {
                try
                {
                    string[] cardInfos = card.Split(';');
                    cards.Add(new DebitCard(cardInfos[0], cardInfos[1], cardInfos[2]));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            foreach (DebitCard card in cards)
            {
                Console.WriteLine(String.Format("Tulajdonos neve:\t{0} {1}", card.HolderName.PadRight(32), card.ValidateCard().ToString().PadRight(5)));
            }

            Console.ReadKey();
        }
    }
}
