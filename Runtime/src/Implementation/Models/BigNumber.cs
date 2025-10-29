using System;
using UnityEngine;

namespace UMF.Core.Implementation
{
    /// <summary>
    ///     Presents numbers like value*(10^n)
    /// </summary>
    [Serializable]
    public struct BigNumber
    {
        [field: SerializeField] public double Number { get; set; }

        [field: SerializeField] public int PowOfTen { get; set; }

        public static BigNumber operator +(BigNumber a, BigNumber b)
        {
            var result = new BigNumber();
            if (a.PowOfTen == b.PowOfTen)
            {
                result.Number = a.Number + b.Number;
                result.PowOfTen = a.PowOfTen;
            }
            else
            {
                var isAMoreB = a.PowOfTen > b.PowOfTen;
                var leftOperand = isAMoreB ? a.Number : b.Number;
                var rightOperand = isAMoreB ? b.Number : a.Number;
                var leftPow = isAMoreB ? a.PowOfTen : b.PowOfTen;
                var rightPow = isAMoreB ? b.PowOfTen : a.PowOfTen;
                var expectedPow = isAMoreB ? a.PowOfTen : b.PowOfTen;
                var powDiff = leftPow - rightPow;

                result.Number = leftOperand + GetSafeOperand(rightOperand, powDiff);
                result.PowOfTen = expectedPow;
            }

            return Validate(result);
        }

        public static BigNumber operator -(BigNumber a, BigNumber b)
        {
            var result = new BigNumber();
            if (a.PowOfTen == b.PowOfTen)
            {
                result.Number = a.Number - b.Number;
                result.PowOfTen = a.PowOfTen;
            }
            else
            {
                var isAMoreB = a.PowOfTen > b.PowOfTen;
                if (isAMoreB)
                {
                    var powDiff = a.PowOfTen - b.PowOfTen;
                    result.Number = a.Number - GetSafeOperand(b.Number, powDiff);
                    result.PowOfTen = a.PowOfTen;
                }
                else
                {
                    var powDiff = b.PowOfTen - a.PowOfTen;
                    result.Number = GetSafeOperand(a.Number, powDiff) - b.Number;
                    result.PowOfTen = b.PowOfTen;
                }
            }

            return Validate(result);
        }

        public static BigNumber operator *(BigNumber a, BigNumber b)
        {
            var result = new BigNumber();
            if (a.PowOfTen == b.PowOfTen)
            {
                var unsafeValue = a.Number * b.Number;
                var castToSafe = Get(unsafeValue);
                result.Number = castToSafe.Number;
                result.PowOfTen = a.PowOfTen + b.PowOfTen + castToSafe.PowOfTen;
            }
            else
            {
                var isAMoreThenB = a.PowOfTen > b.PowOfTen;
                var powDiff = isAMoreThenB ? a.PowOfTen - b.PowOfTen : b.PowOfTen - a.PowOfTen;
                var leftOperand = isAMoreThenB ? a.Number : b.Number;
                var rightOperand = isAMoreThenB ? b.Number : a.Number;
                var expectedPowOfTen = isAMoreThenB ? a.PowOfTen : b.PowOfTen;
                var unsafeValue = leftOperand * GetSafeOperand(rightOperand, powDiff);
                var castToSafe = Get(unsafeValue);
                result.Number = castToSafe.Number;
                result.PowOfTen = expectedPowOfTen + expectedPowOfTen + castToSafe.PowOfTen;
            }

            return Validate(result);
        }

        public static BigNumber operator /(BigNumber a, BigNumber b)
        {
            var result = new BigNumber();
            if (a.PowOfTen == b.PowOfTen)
            {
                var unsafeValue = a.Number / b.Number;
                var castToSafe = Get(unsafeValue);
                result.Number = castToSafe.Number;
                result.PowOfTen = castToSafe.PowOfTen;
            }
            else
            {
                var isAMoreThenB = a.PowOfTen > b.PowOfTen;
                var powDiff = isAMoreThenB ? a.PowOfTen - b.PowOfTen : b.PowOfTen - a.PowOfTen;
                var operandWithTheSamePow = GetSafeOperand(isAMoreThenB ? b.Number : a.Number, powDiff);
                var leftOperand = isAMoreThenB ? a.Number : operandWithTheSamePow;
                var rightOperand = isAMoreThenB ? operandWithTheSamePow : b.Number;
                var expectedPowOfTen = isAMoreThenB ? a.PowOfTen : b.PowOfTen;
                var unsafeValue = leftOperand / rightOperand;
                var castToSafe = Get(unsafeValue);
                result.Number = castToSafe.Number;
                result.PowOfTen = castToSafe.PowOfTen;
            }

            return Validate(result);
        }

        public static BigNumber Pow(BigNumber a, int pow)
        {
            var result = a;
            for (var i = 1; i < pow; i++)
            {
                result *= a;
            }

            return result;
        }

        public static bool operator <(BigNumber a, BigNumber b)
        {
            var validatedFirst = Validate(a);
            var validatedSecond = Validate(b);
            if (validatedFirst.PowOfTen < validatedSecond.PowOfTen)
            {
                return true;
            }

            if (validatedFirst.PowOfTen > validatedSecond.PowOfTen)
            {
                return false;
            }

            if (validatedFirst.PowOfTen == validatedSecond.PowOfTen)
            {
                return validatedFirst.Number < validatedSecond.Number;
            }

            throw new NotImplementedException();
        }

        public static bool operator >(BigNumber a, BigNumber b)
        {
            var isALessB = a < b;
            return !isALessB;
        }

        public override string ToString()
        {
            return $"{Number}*10^{PowOfTen}";
        }

        public static implicit operator BigNumber(int value)
        {
            return Get(value);
        }

        public static implicit operator BigNumber(long value)
        {
            return Get(value);
        }

        public static implicit operator BigNumber(double value)
        {
            return Get(value);
        }

        public static implicit operator BigNumber(float value)
        {
            if (float.IsInfinity(value))
            {
                throw new Exception();
            }

            var pow = 0;
            if (value > 1)
            {
                while (value / 10 > 1)
                {
                    value = value / 10;
                    pow++;
                }
            }

            return new BigNumber { Number = value, PowOfTen = pow };
        }

        public static implicit operator BigNumber(decimal value)
        {
            return Get((float)value);
        }

        private static BigNumber Get(double value)
        {
            var pow = 0;
            if (value > 1)
            {
                while (value / 10 > 1)
                {
                    value = value / 10;
                    pow++;
                }
            }

            return new BigNumber { Number = value, PowOfTen = pow };
        }

        private static BigNumber Validate(BigNumber bigNumber)
        {
            while (bigNumber.Number / 10 >= 1)
            {
                bigNumber.Number = bigNumber.Number / 10;
                bigNumber.PowOfTen++;
            }

            while (bigNumber.Number < 1 && bigNumber.PowOfTen > 0)
            {
                bigNumber.Number = bigNumber.Number * Math.Pow(10, 1);
                bigNumber.PowOfTen -= 1;
            }

            return bigNumber;
        }

        private static double GetSafeOperand(double operand, double powDiff)
        {
            if (powDiff > maxPow)
            {
                var copiedPow = powDiff;
                double res = 0;
                while (copiedPow > maxPow)
                {
                    var w = Math.Pow(10, maxPow);
                    if (Math.Abs(res) < 0.01)
                    {
                        res = operand / w;
                    }
                    else
                    {
                        res /= w;
                    }

                    copiedPow -= maxPow;
                }

                var w1 = Math.Pow(10, copiedPow);
                res /= w1;
                return res;
            }

            return operand / Math.Pow(10, powDiff);
        }

        private const double maxPow = 200;
    }
}