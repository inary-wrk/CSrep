using Xunit;
using App2SearchSort;
using System;
using System.Collections.Generic;
using System.Text;

namespace App2SearchSort.Tests
{
    public class ProgramTests
    {
        [Theory]
        [InlineData(new int[0], 0, -1)]
        [InlineData(new int[] { 5 }, 5, 0)]
        [InlineData(new int[] { 6 }, 5, -1)]
        [InlineData(new int[] { 5, 6, 9, 10, 20, 21 }, 5, 0)]
        [InlineData(new int[] { 5, 6, 9, 10, 20, 21 }, 21, 5)]
        [InlineData(new int[] { 5, 6, 9, 10, 20, 21 }, 20, 4)]
        [InlineData(new int[] { 5, 6, 9, 10, 20, 21 }, 6, 1)]
        [InlineData(new int[] { 5, 6, 9, 10, 20, 21 }, 600, -1)]
        public void BinSearchTest(int[] array, int searchValue, int expectedIndex)
        {
            int actual = Program.BinSearch(array, searchValue);
            Assert.Equal(expectedIndex, actual);
        }
    }
}