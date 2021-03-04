using System;

namespace App2SearchSort
{
    public class Program
    {
        static public int BinSearch(int[] array, int searchValue)
        {
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
    

        static void Main(string[] args)
        {

        }
    }
}
