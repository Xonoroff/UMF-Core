using System;
using Core.src.Entity;
using UnityEngine;

namespace Core.src.Utils
{
    public class BigNumberFormatter : IBigNumberFormatter
    {
        public string Format(BigNumber bigNumber)
        {           
            var format = "{0:#0,0}";
            if (bigNumber.PowOfTen < 3)
            {
                format = "{0:#0}";
            }

            if (bigNumber.PowOfTen >= 4)
            {
                format = "{0:#0.00}";
            }
            
            return string.Format(new BigNumberFormatProvider(), format, bigNumber);
        }

        public string FormatToLower(BigNumber bigNumber)
        {
            var roundingDigit = bigNumber.PowOfTen < 200 ? bigNumber.PowOfTen : 200;
            var zeroes = Math.Pow(10, roundingDigit);
            bigNumber.Number = Math.Floor(bigNumber.Number * zeroes) / zeroes;
            return Format(bigNumber);
        }

        public string FormatToBigger(BigNumber bigNumber)
        {
            var roundingDigit = bigNumber.PowOfTen < 200 ? bigNumber.PowOfTen : 200;
            var zeroes = Math.Pow(10, roundingDigit);
            bigNumber.Number = Math.Ceiling(bigNumber.Number * zeroes) / zeroes;
            return Format(bigNumber);
        }
    }
}