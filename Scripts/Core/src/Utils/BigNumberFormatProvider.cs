using System;
using System.Collections.Generic;
using Core.src.Entity;

namespace Core.src.Utils
{
    public class BigNumberFormatProvider : IFormatProvider, ICustomFormatter
    {
        public object GetFormat(Type formatType)
        {
            if (formatType == typeof(ICustomFormatter))
                return this;
            else
                return null;
        }

        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            if (arg is BigNumber bigNumber)
            {

                if (bigNumber.PowOfTen < 4)
                {
                    var expecteddNumber = bigNumber.Number * Math.Pow(10, bigNumber.PowOfTen); 
                    return expecteddNumber.ToString(format) + GetStringRepresentationByPow(bigNumber.PowOfTen);   
                }
                
                var mod = bigNumber.PowOfTen % 3;
                var result = bigNumber.Number * Math.Pow(10, mod);
                var representation = GetStringRepresentationByPow(bigNumber.PowOfTen);
                return result.ToString(format) + representation;
            }
            else if (arg == null)
            {
                return "0";
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        char[] alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToLower().ToCharArray();
        private int alphabetCount
        {
            get { return alphabet.Length; }
        }

        private string GetStringRepresentationByPow(int pow)
        {
            if (pow < 4)
            {
                return "";
            }
            if (pow < 6)
            {
                return "K";
            }

            if (pow < 9)
            {
                return "M";
            }

            if (pow < 12)
            {
                return "B";
            }

            if (pow < 15)
            {
                return "T";
            }

            var powMap = new Dictionary<int, char>();
            for (int i = 0; i < alphabetCount; i++)
            {
                powMap.Add(i * 3, alphabet[i]);
                powMap.Add(i * 3 + 1, alphabet[i]);
                powMap.Add(i * 3 + 2, alphabet[i]);
            }

            //TODO: improve math skills xD
            var firstLetterIndex = ((pow - 15) / (alphabetCount * 3)) % (alphabetCount * 3);
            var secondLetterIndex = (pow - 15) % (alphabetCount * 3);

            var firstLetter = alphabet[firstLetterIndex];
            var secondLetter = powMap[secondLetterIndex];
            return firstLetter.ToString() + secondLetter; //TODO:
        }
    }
}