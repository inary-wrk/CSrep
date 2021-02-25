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

        //[TestCase(-5, "Простое")]
        //[TestCase(-9, "Не простое")]
        //[TestCase(-1, "Не простое")]
        //[TestCase(0, "Не простое")]
        //[TestCase(1, "Не простое")]
        //[TestCase(6, "Не простое")]
        //[TestCase(2, "Простое")]
        //[TestCase(7, "Простое")]
        Object DataSet = new[] { new { number = -5, expected = "Простое" },
                                 new { number = -9, expected = "Не простое" },
                                 new { number = -1, expected = "Не простое" },
                                 new { number = 0, expected = "Не простое" },
                                 new { number = 1, expected = "Не простое" },
                                 new { number = 2, expected = "Простое" },
                                 new { number = 6, expected = "Не простое" },
                                 new { number = 7, expected = "Простое" },
                               };


        [TestCaseSource(nameof(DataSet))]
        public void PrimeTest(int number, string expected)
        {
            Assert.AreEqual(PrimeNumber.Prime(number), expected);
        }

        public void PrimeWorkedTest(int number, string expected)
        {
            Assert.AreEqual(PrimeNumber.PrimeWorked(number), expected);
        }

    }
}