namespace DebitCardReader.models
{
    public class DebitCard
    {
        /// <summary>
        /// Kártyaszám
        /// </summary>
        public string Number { get; private set; }
        /// <summary>
        /// Kártyatulajdonos neve
        /// </summary>
        public string HolderName { get; private set; }
        /// <summary>
        /// A kártya lejárati dátuma
        /// </summary>
        public DateTime ExpirationDate { get; private set; }

        public DebitCard(string cardNumber, string cardHolderName, string cardExpirationDate)
        {
            // Ellenőrizzük, hogy a kártyaszám megfelelő hosszúságú-e
            if (cardNumber.Length != 16)
            {
                throw new Exception("Invalid card number format!");
            }
            Number = cardNumber;

            // Ellenőrizzük, hogy a kártyatulajdonos neve nem üres-e
            if (cardHolderName.Length < 3)
            {
                throw new Exception("Invalid card holder name format!");
            }

            HolderName = cardHolderName;

            // Ellenőrizzük, hogy a lejárati dátum megfelelő hosszúságú-e
            if (cardExpirationDate.Length != 5)
            {
                throw new Exception("Invalid expiration date format!");
            }

            // Átalakítjuk a lejárati dátumot DateTime típusúvá
            ExpirationDate = DateTime.ParseExact(cardExpirationDate, "MM/yy", null);
            // Sajnos ha 30-at adok meg évnek akkor a C# úgy értelmezi, hogy 1930-at szeretnék megadni, ezért ezt a hibát kijavítjuk
            while (ExpirationDate.Year < 2000)
            {
                ExpirationDate = ExpirationDate.AddYears(100);
            }
        }

        /// <summary>
        /// Ellenőrzi, hogy a kártya megfelel-e minden kritériumnak.
        /// </summary>
        /// <param name="card">A kártya adatai</param>
        /// <returns>Igaz vagy Hamis értéket ad vissza annak függvényében, hogy a kártya megfelel-e minden kritériumnak</returns>
        public bool ValidateCard()
        {
            // Ellenőrizzük, hogy a kártyaszám valós-e
            if (!ValidateCardNumber())
            {
                return false;
            }

            // Ellenőrizzük, hogy a kártya nem járt-e le
            if (!ValidateCardExpirationDate())
            {
                return false;
            }

            // Ha a kártya megfelel minden kritériumnak, akkor igaz értékkel térünk vissza
            return true;
        }

        /// <summary>
        /// A kártyaszámát ellenőrző függvény. A kártyaszám ellenőrzése a Luhn-algoritmus segítségével történik.
        /// </summary>
        /// <returns>Igaz vagy Hamis értéket ad vissza annak függvényében, hogy a kártyaszám valós-e</returns>
        private bool ValidateCardNumber()
        {
            int cardNumberSum = 0;

            for (int i = 0; i < 16; i++)
            {
                int cardNumberDigit = int.Parse(this.Number[i].ToString());
                if (i % 2 == 0)
                {
                    cardNumberDigit *= 2;
                    if (cardNumberDigit > 9)
                    {
                        cardNumberDigit -= 9;
                    }
                }
                cardNumberSum += cardNumberDigit;
            }

            // Ha a kártyaszám osztható 10-zel, akkor igaz értékkel térünk vissza
            return cardNumberSum % 10 == 0;
        }

        /// <summary>
        /// A bankkártya lejárati dátumának ellenőrzése.
        /// </summary>
        /// <returns>Igaz vagy Hamis értéket ad vissza annak függvényében, hogy a kártya nem járt-e le.</returns>
        private bool ValidateCardExpirationDate()
        {
            // Ellenőrizzük, hogy a kártya nem járt-e le
            if (this.ExpirationDate < DateTime.Now)
            {
                return false;
            }

            // Ha nem járt le, akkor igaz értékkel térünk vissza
            return true;
        }
    }
}
