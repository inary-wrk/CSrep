// 6. Для полного закрепления понимания простых типов найдите любой чек, либо фотографию этого чека в интернете и схематично нарисуйте его в консоли, 
// только за место динамических, по вашему мнению, данных (это может быть дата, название магазина, сумма покупок) подставляйте переменные, которые 
// были заранее заготовлены до вывода на консоль.
// Фотография, по которой делал, в папке с программой 

using System;
using App4CashierCheck.Classes;

namespace App4CashierCheck
{

    class Program
    {
        static void Main(string[] args)
        {
            var myShop = new Shop();
            var myReceipt = new Receipt();


            // Console output

            Console.Clear();
            Console.SetCursorPosition(30, 0);
            Console.Write(myShop.shopName);
            Console.SetCursorPosition(30, 1);
            Console.Write("Чек №:{0}", myReceipt.number);
            Console.SetCursorPosition(30, 2);
            Console.Write("Кассир:{0}", myShop.cashierName);

            int i = 1;
            int b = -3;
            foreach (Product item in myReceipt.products)
            {
                b += 4;
                Console.SetCursorPosition(20, 4 + b);
                Console.Write($"{i}. {item.name}");
                Console.SetCursorPosition(20, 5 + b);
                Console.Write($"{item.quantity} X {item.price.ToString("N")}");
                Console.SetCursorPosition(20, 6 + b);
                Console.Write($"Стоимостью................{item.totalPrice.ToString("N")}");
                i++;
            }
            Console.SetCursorPosition(15, b + 8);
            Console.WriteLine("============================================");
            Console.SetCursorPosition(20, b + 10);
            Console.WriteLine($"Всего................{myReceipt.receiptTotalPrice.ToString("N")}");
            Console.SetCursorPosition(20, b + 12);
            Console.WriteLine($"ККМ: {myShop.data.kKM}  ИНН {myShop.data.iNN} №{myReceipt.number}");
            Console.SetCursorPosition(20, b + 14);
            Console.WriteLine($"{myReceipt.currentTime}  Кассир {myShop.cashierName}");
            Console.SetCursorPosition(20, b + 16);
            Console.WriteLine($"Итог:                   ={myReceipt.receiptTotalPrice.ToString("N")}");
            Console.SetCursorPosition(30, b + 18);
            Console.WriteLine($"ЭКЛЗ {myShop.data.numEKLZ}  {myShop.data.idEKLZ}");
            Console.ReadLine();
        }
    }
}
