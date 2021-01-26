// 2. Написать программу — телефонный справочник — создать двумерный массив 5*2, хранящий список телефонных контактов:
// первый элемент хранит имя контакта, второй — номер телефона/e-mail.
using System;

namespace App2Phonebook
{
    class Program
    {
        static void Main(string[] args)
        {
            string[,] phonebook = new string[5, 2];
            int dim0 = phonebook.GetUpperBound(0);
            int dim1 = phonebook.GetUpperBound(1);
            int indentLength = 0;
            for (int i = 0; i <= dim0; i++)
            {
                Console.SetCursorPosition(0, i * 2);
                Console.Write($"{i + 1}. Enter contact name: ");
                phonebook[i, 0] = Console.ReadLine();
                Console.Write("Enter phone number/email: ");
                phonebook[i, 1] = Console.ReadLine();

                // console output
                int nIndentLength = 0;
                for (int j = 0; j <= dim1; j++)
                {
                    if (nIndentLength < phonebook[i, j].Length) nIndentLength = phonebook[i, j].Length;
                }

                Console.SetCursorPosition(indentLength + 3 + nIndentLength / 2, (dim0 + 1) * 2);
                Console.WriteLine(i + 1);
                Console.SetCursorPosition(indentLength, (dim0 + 2) * 2);
                Console.WriteLine(" | {0}", phonebook[i, 0]);
                Console.SetCursorPosition(indentLength, (dim0 + 2) * 2 + 1);
                Console.WriteLine(" | {0}", phonebook[i, 1]);

                indentLength += nIndentLength + 3;

            }

            Console.ReadLine();
        }
    }
}
