public static class CreditCardLunhAlgorithm
{
     public static class PayService
    {
        /// <summary>
        /// Cleans card Numkber
        /// </summary>
        /// <param name="cardNumber"></param>
        /// <returns></returns>
        private static string CleanCardNumber(string cardNumber)
        {
            var cleanNumber = cardNumber.Replace("-", "").Replace(" ", "").Replace("(", "").Replace(")", "").Replace("_", ""); ;
            return cleanNumber;
        }
        /// <summary>
        /// Length control
        /// </summary>
        /// <param name="cardNumber"></param>
        /// <returns></returns>
        private static bool CardNoLengthControl(string cardNumber)
        {
            if (cardNumber.Length == 16) return true;
            return false;
        }
        /// <summary>
        /// control that everything in the cardNumber is number
        /// </summary>
        /// <param name="cardNumber"></param>
        /// <returns></returns>
        private static bool CardNumberControl(string cardNumber)
        {
            foreach (var item in cardNumber)
            {
                if (!Char.IsNumber(item)) return false;
            }
            return true;
        }

        /// <summary>
        /// Gives sum of digits 
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        private static int GatherDigits(int number)
        {
            int sum = 0;
            while (number > 0)
            {
                sum += number % 10;
                number /= 10;
            }
            return sum;
        }


        /// <summary>
        /// Lunh Algorithm
        /// </summary>
        /// <param name="cardNumber"></param>
        /// <returns></returns>
        public static bool LunhAlgorithm(string cardNumber)
        {
            cardNumber = CleanCardNumber(cardNumber);

            if (CardNoLengthControl(cardNumber) && CardNumberControl(cardNumber)) 
            {
                 int sumOfEvens = 0;
                 int sumOfOds = 0;

                 for(int i = 0; i < cardNumber.Length; i++)
                 {
                     int element = Convert.ToInt32(cardNumber[i].ToString());

                     if(i % 2 == 0)
                     {
                         sumOfEvens += GatherDigits(element * 2);
                     }
                     else
                     {
                         sumOfOds += element;
                     }
                 }

                 var result = (sumOfEvens + sumOfOds) % 10;

                 if (result == 0) 
                      return true;
                 return false;
            }
            return false;
        }
    }
}
