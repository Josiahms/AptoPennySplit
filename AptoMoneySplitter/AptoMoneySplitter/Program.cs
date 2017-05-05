using System;
using System.Linq;

namespace AptoMoneySplitter
{
   /// <summary>
   /// The money splitter class provides public functionality to split a specified amount of money
   /// between a specified number of people.  This class contains an entry point which can be run
   /// to test the functionality, but the class should be robust enough to integrate with other code
   /// as a stand-alone "black-box" component if need be.
   /// </summary>
   class MoneySplitter
   {
      private const decimal AMT_TO_SPLIT = 800.0m;       //The amount of money to split.
      private const decimal ONE_CENT = 0.01m;            //The decimal value of 1 cent.
      private const int DEC_PLACES = 2;                  //The number of decimal places to round to.  Must be greater than 0.
      private const int NUM_PEOPLE = 3;                  //Number of people to split the money between.  Must be greater than 0.


      /// <summary>
      /// Entry point of the program.  Calls the moneySplitter method and then
      /// exits execution.
      /// </summary>
      /// <param name="args"></param>
      static void Main(string[] args)
      {
         MoneySplitter m = new MoneySplitter();
         m.moneySplitter(AMT_TO_SPLIT, NUM_PEOPLE);
      }

      /// <summary>
      /// Takes in an amount of money and the number of people to split it between and prints
      /// the values of the money split between each person before and after its value has been
      /// validated.
      /// </summary>
      /// <param name="total">The total amount of money to start with.</param>
      /// <param name="numPeople">The number of people to split between.</param>
      public void moneySplitter(decimal total, int numPeople)
      {
         decimal[] splitValues = split(total, numPeople);
         Console.WriteLine(string.Join(" ", splitValues));

         validate(total, splitValues);
         Console.WriteLine(string.Join(" ", splitValues));
      }

      /// <summary>
      /// Takes in a dollar amount and the number of people to split it between and
      /// returns an array of the size of numPeople with the dollar amount divided
      /// evenly into each array element rounded to DEC_PLACES decimal places.
      /// </summary>
      /// <param name="total">The total amount of money to split.</param>
      /// <param name="numPeople">The number of people to split it between.</param>
      /// <returns>An array with the money split between the number of people.</returns>
      private decimal[] split(decimal total, int numPeople)
      {
         if (numPeople <= 0)
            return null;

         var splitValue = Math.Round(total / numPeople, DEC_PLACES);
         var result = new decimal[numPeople];
         for(int i = 0; i < numPeople; i++) { result[i] = splitValue; }
         return result;
      }

      /// <summary>
      /// Validates that the values in the specified array add up to the desired total.  If
      /// they do not, then a penny is added or subtracted in a "round-robin" fashion to each
      /// element until the desired value is achieved.
      /// </summary>
      /// <param name="desiredTotal">The total that the values should sum up to.</param>
      /// <param name="values">The values to validate.</param>
      private void validate(decimal desiredTotal, decimal[] values)
      {
         if (values == null)
            return;

         decimal difference = Math.Round(values.Sum(), DEC_PLACES) - desiredTotal;

         int i = 0;
         while (difference != 0)
         {
            if (difference < 0)
            {
               values[i] += ONE_CENT;
               difference += ONE_CENT;
            }
            else
            {
               values[i] -= ONE_CENT;
               difference -= ONE_CENT;
            }
            i = (i + 1) % values.Length;
         }
      }
   }
}
