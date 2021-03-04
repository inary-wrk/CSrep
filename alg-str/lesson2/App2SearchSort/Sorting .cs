using System;
using System.Collections.Generic;
using System.Text;

namespace App2SearchSort
{
    public class Sorting
    {


        public static int[] BubbleSort(int[] array)
        {
            int temp;
            int brkCounter = 1;
            for (int j = array.Length - 1; j > 0; j--)
            {
                if (brkCounter == 0) break;
                brkCounter = 0;
                for (int i = 0; i < j; i++)
                {
                    if (array[i] > array[i + 1])
                    {
                        temp = array[i + 1];
                        array[i + 1] = array[i];
                        array[i] = temp;
                        brkCounter++;
                    }
                }
            }
            return array;
        }

        public static int[] CombSort(int[] array)
        {
            int distance = array.Length - 1;
            while (distance!=0)
            {
                for (int i = 0; i < distance; i++)
                {
                    
                }
            }
            return array;
        }

    }
}
