using System;

namespace App2O_N_
{
    class Program
    {
        public static int StrangeSum(int[] inputArray) // O(N^3) опускаем константы
        {
            // O(2+3*N^3)
            int sum = 0; // O(1)
            for (int i = 0; i < inputArray.Length; i++) // O(3*N^3)
            {
                for (int j = 0; j < inputArray.Length; j++)  // O(3*N^2)
                {
                    for (int k = 0; k < inputArray.Length; k++) // O(3*N)
                    {
                        int y = 0; // O(1)

                        if (j != 0)
                        {
                            y = k / j; // O(1)
                        }

                        sum += inputArray[i] + i + k + j + y; // O(1)
                    }
                }
            }

            return sum; // O(1)
        }

    }
}
