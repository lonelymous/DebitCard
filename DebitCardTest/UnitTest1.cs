using Microsoft.VisualStudio.TestPlatform.TestHost;
using DebitCardReader.models;

namespace DebitCardTest
{
    public class Tests
    {
        private DebitCard _debitCard;

        /// <summary>
        ///  Beállítunk egy teszt kártyát
        /// </summary>
        [SetUp]
        public void Setup()
        {
            // Teszt kártya
            _debitCard = new DebitCard("1034135710341357", "Teszt","08/30");
        }

        /// <summary>
        /// Unit teszt a kártyaszám validálására 
        /// </summary>
        [Test]
        public void ValidCard()
        {
            bool result = _debitCard.ValidateCard();
            Assert.IsTrue(result);
        }
    }
}