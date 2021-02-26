using NUnit.Framework;
using App3Fibonacci;
using System;
using System.Collections.Generic;
using System.Text;

namespace App3Fibonacci.Tests
{
    [TestFixture()]
    public class ProgramTests
    {


        [TestCase(0, 0)]
        [TestCase(1, 1)]
        [TestCase(1, 2)]
        [TestCase(21, 8)]
        [TestCase(-5, -5)]
        [TestCase(-34, -9)]
        public void FibonacciRecTest(int expected, int number)
        {
            Assert.AreEqual(expected, Program.FibonacciRec(number));
        }


        [TestCase(0, 0)]
        [TestCase(1, 1)]
        [TestCase(1, 2)]
        [TestCase(21, 8)]
        [TestCase(-5, -5)]
        [TestCase(-34, -9)]
        public void FibonacciLoopTest(int expected, int number)
        {
            Assert.AreEqual(expected, Program.FibonacciLoop(number));
        }
    }
}