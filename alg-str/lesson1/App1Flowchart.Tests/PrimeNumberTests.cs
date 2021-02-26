using NUnit.Framework;
using App1Flowchart;
using System;
using System.Collections.Generic;
using System.Text;


namespace App1Flowchart.Tests
{
    [TestFixture()]
    public class PrimeNumberTests
    {

        static object[] DataSet = new object[]
                                        {
                                          new object[] { -9, "Не простое" },
                                          new object[] { -1, "Не простое" },
                                          new object[] { 0, "Не простое" },
                                          new object[] { 1, "Не простое" },
                                          new object[] { 2, "Простое" },
                                          new object[] { 6, "Не простое" },
                                          new object[] { 7, "Простое" },
                                        };


        [TestCaseSource(nameof(DataSet))]
        public void PrimeTest(int number, string expected)
        {
            Assert.AreEqual(expected, PrimeNumber.PrimeMy(number));
        }


        [TestCaseSource(nameof(DataSet))]
        public void PrimeMyTest(int number, string expected)
        {
            Assert.AreEqual(expected, PrimeNumber.PrimeMy(number));
        }

    }
}