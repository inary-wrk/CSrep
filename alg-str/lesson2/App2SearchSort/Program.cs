using System;

namespace App2SearchSort
{
    public class Program
    {
        static public int BinSearch(int[] array, int searchValue) => BinSearch(array, searchValue, false);
        static public int BinSearch(int[] array, int searchValue, bool sort) // O(log n)
        {
            if (sort) Sorting.CombSort(array);

            int min = 0;
            int max = array.Length - 1;
            while (min <= max)
            {
                int mid = (min + max) / 2;
                if (searchValue == array[mid])
                {
                    return mid;
                }
                else if (searchValue < array[mid])
                {
                    max = mid - 1;
                }
                else
                {
                    min = mid + 1;
                }
            }
            return -1;
        }


        static void Main()
        {

        }
    }
}
