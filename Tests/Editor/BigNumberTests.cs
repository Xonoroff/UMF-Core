using System;
using System.Collections.Generic;
using System.Numerics;
using Core.src.Entity;
using Core.src.Utils;
using NUnit.Framework;
using UMF.Core.Implementation;
using UMF.Core.Implementation.Formatters;
using UnityEngine;
using Random = System.Random;

namespace Core.test.EditMode
{
    internal class BigNumberTests
    {
        private const double delta = 0.1;
        
        private static IEnumerable<TestCaseData> TestCasesForSum()
        {
            var value_1 = 1.56843;
            var value_2 = 3.65766;
            var defaultPow = 1;
            for (var pow = 1; pow < 200; pow++)
            {
                //same number but different pows
                var expectedResult_1 = (value_1 * Math.Pow(10, defaultPow)) + (value_1 * Math.Pow(10, pow));
                var bigNumber_1 = new BigNumber() {Number = value_1, PowOfTen = defaultPow};
                var bigNumber_2 = new BigNumber() {Number = value_1, PowOfTen = pow};
                yield return new TestCaseData(bigNumber_1, bigNumber_2, expectedResult_1).SetName($"[0]Check add operator {bigNumber_1} + {bigNumber_2} = {expectedResult_1}");

                var expectedResult_2 = (value_1 * Math.Pow(10, defaultPow)) + (value_1 * Math.Pow(10, pow));
                var bigNumber_3 = new BigNumber() {Number = value_1, PowOfTen = pow};
                var bigNumber_4 = new BigNumber() {Number = value_1, PowOfTen = defaultPow};
                yield return new TestCaseData(bigNumber_3, bigNumber_4, expectedResult_2).SetName($"[1]Check add operator {bigNumber_3} + {bigNumber_4} = {expectedResult_2}");
                
                //same pow but different numbers
                var expectedResult_3 = (value_1 * Math.Pow(10, pow)) + (value_2 * Math.Pow(10, pow));
                var bigNumber_5 = new BigNumber(){Number = value_1, PowOfTen = pow};
                var bigNumber_6 = new BigNumber() {Number = value_2, PowOfTen = pow};
                yield return new TestCaseData(bigNumber_5, bigNumber_6, expectedResult_3).SetName($"[2]Check add operator {bigNumber_5} + {bigNumber_6} = {expectedResult_3}");

                var expectedResult_4 = (value_1 * Math.Pow(10, pow)) + (value_2 * Math.Pow(10, pow));
                var bigNumber_7 = new BigNumber(){Number = value_2, PowOfTen = pow};
                var bigNumber_8 = new BigNumber(){Number = value_1, PowOfTen = pow};
                yield return new TestCaseData(bigNumber_7, bigNumber_8, expectedResult_4).SetName($"[3]Check add operator {bigNumber_7} + {bigNumber_8} = {expectedResult_4}");
                
                //different pow and different number
                var expectedResult_5 = (value_1 * Math.Pow(10, pow)) + (value_2 * Math.Pow(10, pow - 1));
                var bigNumber_9 = new BigNumber() {Number = value_1, PowOfTen = pow};
                var bigNumber_10 = new BigNumber() {Number = value_2, PowOfTen = pow - 1};
                yield return new TestCaseData(bigNumber_9, bigNumber_10, expectedResult_5).SetName($"[4]Check add operator {bigNumber_9} + {bigNumber_10} = {expectedResult_5}");

                var expectedResult_6 = (value_1 * Math.Pow(10, pow - 1)) + (value_2 * Math.Pow(10, pow));
                var bigNumber_11 = new BigNumber() {Number = value_2, PowOfTen = pow};
                var bigNumber_12 = new BigNumber() {Number = value_1, PowOfTen = pow - 1};
                yield return new TestCaseData(bigNumber_11, bigNumber_12, expectedResult_6).SetName($"[5]Check add operator {bigNumber_11} + {bigNumber_12} = {expectedResult_6}");
            }
        }
        
        private static IEnumerable<TestCaseData> TestCasesForDifference()
        {
            var value_1 = 1.56843;
            var value_2 = 3.65766;
            var defaultPow = 1;
            for (var pow = 1; pow < 200; pow++)
            {
                //same number but different pows
                var expectedResult_1 = (value_1 * Math.Pow(10, defaultPow)) - (value_1 * Math.Pow(10, pow));
                var bigNumber_1 = new BigNumber() {Number = value_1, PowOfTen = defaultPow};
                var bigNumber_2 = new BigNumber() {Number = value_1, PowOfTen = pow};
                yield return new TestCaseData(bigNumber_1, bigNumber_2, expectedResult_1).SetName($"[0]Check diff operator {bigNumber_1} - {bigNumber_2} = {expectedResult_1}");

                var expectedResult_2 = (value_1 * Math.Pow(10, pow)) - (value_1 * Math.Pow(10, defaultPow));
                var bigNumber_3 = new BigNumber() {Number = value_1, PowOfTen = pow};
                var bigNumber_4 = new BigNumber() {Number = value_1, PowOfTen = defaultPow};
                yield return new TestCaseData(bigNumber_3, bigNumber_4, expectedResult_2).SetName($"[1]Check diff operator {bigNumber_3} - {bigNumber_4} = {expectedResult_2}");
                
                //same pow but different numbers
                var expectedResult_3 = (value_1 * Math.Pow(10, pow)) - (value_2 * Math.Pow(10, pow));
                var bigNumber_5 = new BigNumber(){Number = value_1, PowOfTen = pow};
                var bigNumber_6 = new BigNumber() {Number = value_2, PowOfTen = pow};
                yield return new TestCaseData(bigNumber_5, bigNumber_6, expectedResult_3).SetName($"[2]Check diff operator {bigNumber_5} - {bigNumber_6} = {expectedResult_3}");

                var expectedResult_4 = (value_2 * Math.Pow(10, pow)) - (value_1 * Math.Pow(10, pow));
                var bigNumber_7 = new BigNumber(){Number = value_2, PowOfTen = pow};
                var bigNumber_8 = new BigNumber(){Number = value_1, PowOfTen = pow};
                yield return new TestCaseData(bigNumber_7, bigNumber_8, expectedResult_4).SetName($"[3]Check diff operator {bigNumber_7} - {bigNumber_8} = {expectedResult_4}");
                
                //different pow and different number
                var expectedResult_5 = (value_1 * Math.Pow(10, pow)) - (value_2 * Math.Pow(10, pow - 1));
                var bigNumber_9 = new BigNumber() {Number = value_1, PowOfTen = pow};
                var bigNumber_10 = new BigNumber() {Number = value_2, PowOfTen = pow - 1};
                yield return new TestCaseData(bigNumber_9, bigNumber_10, expectedResult_5).SetName($"[4]Check diff operator {bigNumber_9} - {bigNumber_10} = {expectedResult_5}");

                var expectedResult_6 = (value_2 * Math.Pow(10, pow)) - (value_1 * Math.Pow(10, pow - 1));
                var bigNumber_11 = new BigNumber() {Number = value_2, PowOfTen = pow};
                var bigNumber_12 = new BigNumber() {Number = value_1, PowOfTen = pow - 1};
                yield return new TestCaseData(bigNumber_11, bigNumber_12, expectedResult_6).SetName($"[5]Check diff operator {bigNumber_11} - {bigNumber_12} = {expectedResult_6}");
            }
        }
        
        private static IEnumerable<TestCaseData> TestCasesForMult()
        {
            var value_1 = 1.56843;
            var value_2 = 3.65766;
            var defaultPow = 1;
            for (var pow = 1; pow < 100; pow++)
            {
                //same number but different pows
                var expectedResult_1 = (value_1 * Math.Pow(10, defaultPow)) * (value_1 * Math.Pow(10, pow));
                var bigNumber_1 = new BigNumber() {Number = value_1, PowOfTen = defaultPow};
                var bigNumber_2 = new BigNumber() {Number = value_1, PowOfTen = pow};
                yield return new TestCaseData(bigNumber_1, bigNumber_2, expectedResult_1).SetName($"[0]Check mult operator {bigNumber_1} + {bigNumber_2} = {expectedResult_1}");

                var expectedResult_2 = (value_1 * Math.Pow(10, defaultPow)) * (value_1 * Math.Pow(10, pow));
                var bigNumber_3 = new BigNumber() {Number = value_1, PowOfTen = pow};
                var bigNumber_4 = new BigNumber() {Number = value_1, PowOfTen = defaultPow};
                yield return new TestCaseData(bigNumber_3, bigNumber_4, expectedResult_2).SetName($"[1]Check mult operator {bigNumber_3} + {bigNumber_4} = {expectedResult_2}");
                
                //same pow but different numbers
                var expectedResult_3 = (value_1 * Math.Pow(10, pow)) * (value_2 * Math.Pow(10, pow));
                var bigNumber_5 = new BigNumber(){Number = value_1, PowOfTen = pow};
                var bigNumber_6 = new BigNumber() {Number = value_2, PowOfTen = pow};
                yield return new TestCaseData(bigNumber_5, bigNumber_6, expectedResult_3).SetName($"[2]Check mult operator {bigNumber_5} + {bigNumber_6} = {expectedResult_3}");

                var expectedResult_4 = (value_1 * Math.Pow(10, pow)) * (value_2 * Math.Pow(10, pow));
                var bigNumber_7 = new BigNumber(){Number = value_2, PowOfTen = pow};
                var bigNumber_8 = new BigNumber(){Number = value_1, PowOfTen = pow};
                yield return new TestCaseData(bigNumber_7, bigNumber_8, expectedResult_4).SetName($"[3]Check mult operator {bigNumber_7} + {bigNumber_8} = {expectedResult_4}");
                
                //different pow and different number
                var expectedResult_5 = (value_1 * Math.Pow(10, pow)) * (value_2 * Math.Pow(10, pow - 1));
                var bigNumber_9 = new BigNumber() {Number = value_1, PowOfTen = pow};
                var bigNumber_10 = new BigNumber() {Number = value_2, PowOfTen = pow - 1};
                yield return new TestCaseData(bigNumber_9, bigNumber_10, expectedResult_5).SetName($"[4]Check mult operator {bigNumber_9} + {bigNumber_10} = {expectedResult_5}");

                var expectedResult_6 = (value_1 * Math.Pow(10, pow - 1)) * (value_2 * Math.Pow(10, pow));
                var bigNumber_11 = new BigNumber() {Number = value_2, PowOfTen = pow};
                var bigNumber_12 = new BigNumber() {Number = value_1, PowOfTen = pow - 1};
                yield return new TestCaseData(bigNumber_11, bigNumber_12, expectedResult_6).SetName($"[5]Check mult operator {bigNumber_11} + {bigNumber_12} = {expectedResult_6}");
            }
        }            
        
        private static IEnumerable<TestCaseData> TestCasesForDivide()
        {
            var value_1 = 1.56843;
            var value_2 = 3.65766;
            var defaultPow = 1;
            for (var pow = 1; pow < 100; pow++)
            {
                //same number but different pows
                var expectedResult_1 = (value_1 * Math.Pow(10, defaultPow)) / (value_1 * Math.Pow(10, pow));
                var bigNumber_1 = new BigNumber() {Number = value_1, PowOfTen = defaultPow};
                var bigNumber_2 = new BigNumber() {Number = value_1, PowOfTen = pow};
                yield return new TestCaseData(bigNumber_1, bigNumber_2, expectedResult_1).SetName($"[0]Check divide operator {bigNumber_1} + {bigNumber_2} = {expectedResult_1}");

                var expectedResult_2 = (value_1 * Math.Pow(10, pow)) / (value_1 * Math.Pow(10, defaultPow));
                var bigNumber_3 = new BigNumber() {Number = value_1, PowOfTen = pow};
                var bigNumber_4 = new BigNumber() {Number = value_1, PowOfTen = defaultPow};
                yield return new TestCaseData(bigNumber_3, bigNumber_4, expectedResult_2).SetName($"[1]Check divide operator {bigNumber_3} + {bigNumber_4} = {expectedResult_2}");
                
                //same pow but different numbers
                var expectedResult_3 = (value_1 * Math.Pow(10, pow)) / (value_2 * Math.Pow(10, pow));
                var bigNumber_5 = new BigNumber(){Number = value_1, PowOfTen = pow};
                var bigNumber_6 = new BigNumber() {Number = value_2, PowOfTen = pow};
                yield return new TestCaseData(bigNumber_5, bigNumber_6, expectedResult_3).SetName($"[2]Check divide operator {bigNumber_5} + {bigNumber_6} = {expectedResult_3}");

                var expectedResult_4 = (value_2 * Math.Pow(10, pow)) / (value_1 * Math.Pow(10, pow));
                var bigNumber_7 = new BigNumber(){Number = value_2, PowOfTen = pow};
                var bigNumber_8 = new BigNumber(){Number = value_1, PowOfTen = pow};
                yield return new TestCaseData(bigNumber_7, bigNumber_8, expectedResult_4).SetName($"[3]Check divide operator {bigNumber_7} + {bigNumber_8} = {expectedResult_4}");
                
                //different pow and different number
                var expectedResult_5 = (value_1 * Math.Pow(10, pow)) / (value_2 * Math.Pow(10, pow - 1));
                var bigNumber_9 = new BigNumber() {Number = value_1, PowOfTen = pow};
                var bigNumber_10 = new BigNumber() {Number = value_2, PowOfTen = pow - 1};
                yield return new TestCaseData(bigNumber_9, bigNumber_10, expectedResult_5).SetName($"[4]Check divide operator {bigNumber_9} + {bigNumber_10} = {expectedResult_5}");

                var expectedResult_6 = (value_2 * Math.Pow(10, pow)) / (value_1 * Math.Pow(10, pow - 1));
                var bigNumber_11 = new BigNumber() {Number = value_2, PowOfTen = pow};
                var bigNumber_12 = new BigNumber() {Number = value_1, PowOfTen = pow - 1};
                yield return new TestCaseData(bigNumber_11, bigNumber_12, expectedResult_6).SetName($"[5]Check divide operator {bigNumber_11} + {bigNumber_12} = {expectedResult_6}");
            }
        }      
        
        private static IEnumerable<TestCaseData> TestCasesForPow()
        {
            var value_1 = 1.56843;
            var defaultPow = 0;
            for (var pow = 1; pow < 8; pow++)
            {
                //same number but different pows
                var bigNumber_1 = new BigNumber(){Number = value_1, PowOfTen = defaultPow};
                var expectedResult_1 = Math.Pow((double)value_1, pow);
                yield return new TestCaseData(bigNumber_1, pow, expectedResult_1).SetName($"[0]Check Pow({bigNumber_1}, {pow}) = {expectedResult_1}"); 
            }
        }

        private static IEnumerable<TestCaseData> TestCasesForLess()
        {
            yield return new TestCaseData(new BigNumber(){Number = 1, PowOfTen = 0}, new BigNumber(){ Number = 1, PowOfTen = 1}, true);
            yield return new TestCaseData(new BigNumber(){Number = 10, PowOfTen = 1}, new BigNumber(){ Number = 100, PowOfTen = 1}, true);
            yield return new TestCaseData(new BigNumber(){Number = 1, PowOfTen = 3}, new BigNumber(){ Number = 10000, PowOfTen = 0}, true);
        }
        
        private static IEnumerable<TestCaseData> TestCaseDataForAddMethod()
        {
            
            var baseValue = 1.44487874512;
            for (int powOfTen = 1; powOfTen < 20; powOfTen++)
            {
                var val = baseValue * Mathf.Pow(10, powOfTen);
                yield return new TestCaseData(val, new BigNumber(){Number = baseValue, PowOfTen = powOfTen}).SetName($"Value { val }");
            }
        }
        
        private static IEnumerable<TestCaseData> TestCasesForFormatting()
        {
            var number = 1.97955;
            for (int powOfTen = 0; powOfTen < 200; powOfTen++)
            {
                var bigNumber_1 = new BigNumber() {Number = number, PowOfTen = powOfTen};
                var rawValue = number * Math.Pow(10, powOfTen);
                yield return new TestCaseData(bigNumber_1).SetName($"Formatting for {bigNumber_1} raw is {rawValue}");
            }
            
            yield return new TestCaseData((BigNumber)15.28f).SetName($"Formatting for {15.28f}");
            yield return new TestCaseData((BigNumber)12.3909).SetName($"Formatting for {12.3909}");
            yield return new TestCaseData((BigNumber)12.4800).SetName($"Formatting for {12.4800}");
            yield return new TestCaseData((BigNumber)1000.15).SetName($"Formatting for {1000.15}");
            yield return new TestCaseData((BigNumber)15000.13).SetName($"Formatting for {15000.13}");
            yield return new TestCaseData((BigNumber)15835).SetName($"Formatting for {15835}");
            yield return new TestCaseData((BigNumber)150390).SetName($"Formatting for {150390}");
        }        
        
        private static IEnumerable<TestCaseData> TestCaseForPow()
        {
            var number = 1.97955;
            for (int powOfTen = 1; powOfTen < 200; powOfTen++)
            {
                var bigNumber_1 = new BigNumber() {Number = number, PowOfTen = powOfTen};
                yield return new TestCaseData(bigNumber_1);
            }
        }

        
        [Test]
        [TestCaseSource("TestCaseDataForAddMethod")]
        public void Implicit_Float_CastedIntToBigNumber(double value, BigNumber expectedResult)
        {
            BigNumber bigNumber = value;
            
            Assert.That(expectedResult.Number - bigNumber.Number, Is.LessThan(value * 0.001));
            Assert.AreEqual(expectedResult.PowOfTen, bigNumber.PowOfTen);
        }
        
        [Test]
        [TestCaseSource("TestCasesForSum")]
        public void CheckSum(BigNumber a, BigNumber b, double expectedResult)
        {
            var resultOfSum = a + b;
            
            Assert.AreEqual(expectedResult, resultOfSum.Number * Math.Pow(10,resultOfSum.PowOfTen), expectedResult * delta);
        }       
        
        [Test]
        [TestCaseSource("TestCasesForMult")]
        public void CheckMult(BigNumber a, BigNumber b, double expectedResult)
        {
            var resultOfSum = a * b;
            
            Assert.AreEqual(expectedResult, resultOfSum.Number * Math.Pow(10,resultOfSum.PowOfTen),expectedResult * delta);
        }
        
        [TestCaseSource("TestCasesForDivide")]
        public void CheckDivide(BigNumber a, BigNumber b, double expectedResult)
        {
            var resultOfSum = a / b;
            
            Assert.AreEqual(expectedResult, resultOfSum.Number * Math.Pow(10,resultOfSum.PowOfTen),expectedResult * delta);
        }
        
        [Test]
        [TestCaseSource("TestCasesForPow")]
        public void CheckPow(BigNumber a, int pow, double expectedResult)
        {
            var resultOfPow = BigNumber.Pow(a, pow);
            
            Assert.AreEqual(expectedResult, resultOfPow.Number * Math.Pow(10,resultOfPow.PowOfTen), expectedResult * delta);
        }

        [Test]
        [TestCaseSource("TestCasesForLess")]
        public void CheckLow(BigNumber a, BigNumber b, bool expectedResult)
        {
            var result = a < b;
            
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        [TestCaseSource("TestCasesForDifference")]
        public void CheckDiff(BigNumber a, BigNumber b, double expectedResult)
        {
            var result = a - b;
            var actual = result.Number * Math.Pow(10, result.PowOfTen);
            
            Assert.AreEqual(expectedResult, actual, Math.Abs(expectedResult * delta));
        }

        [Test]
        [TestCaseSource("TestCasesForFormatting")]
        public void CheckFormatting(BigNumber bigNumber)
        {
            var formatter = new BigNumberFormatter();
            Debug.Log(formatter.Format(bigNumber) + "Default formatting");
            Debug.Log(formatter.FormatToBigger(bigNumber) + "To Bigger formatting");
            Debug.Log(formatter.FormatToLower(bigNumber) + "To Lower formatting");
        }
    }
}
