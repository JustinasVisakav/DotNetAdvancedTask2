using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public static class FileName
    {
        public static bool TestFunction(long incomingInteger)
        {
            var numberInString = incomingInteger.ToString();
            var placeholderString = numberInString;

            for (int i = 0; i < numberInString.Length/2; i++)
            {

                var firstNumber = placeholderString.First();
                var lastNumber = placeholderString.Last();
                if(firstNumber != lastNumber)
                    return false;
                if (placeholderString.Length > 1)
                {
                    placeholderString= placeholderString.Remove(0, 1);
                    placeholderString= placeholderString.Remove(placeholderString.Length - 1, 1);
                }
            }
            return true;
        }
    }
}
