using Xunit;
using App2SearchSort;
using System;
using System.Collections.Generic;
using System.Text;

namespace App2SearchSort.Tests
{
    public class SortingTests
    {
        [Theory]
        [InlineData(new int[0], new int[0])]
        [InlineData(new int[] { 5 }, new int[] { 5 })]
        [InlineData(new int[] { 5, 4, 3, 2, 1, 0 }, new int[] { 0, 1, 2, 3, 4, 5 })]
        [InlineData(new int[] { 0, 1, 4, 2, 3, 5 }, new int[] { 0, 1, 2, 3, 4, 5 })]
        public void BubbleSortTest(int[] array, int[] expectedSortArray)
        {
            Sorting.BubbleSort(array);
            Assert.Equal(expectedSortArray, array);
        }


        [Theory]
        [InlineData(new int[0], new int[0])]
        [InlineData(new int[] { 5 }, new int[] { 5 })]
        [InlineData(new int[] { 5, 4, 3, 2, 1, 0 }, new int[] { 0, 1, 2, 3, 4, 5 })]
        [InlineData(new int[] { 0, 1, 4, 2, 3, 5 }, new int[] { 0, 1, 2, 3, 4, 5 })]
        public void CombSortTest(int[] array, int[] expectedSortArray)
        {
            Sorting.CombSort(array);
            Assert.Equal(expectedSortArray, array);
        }
    }
}