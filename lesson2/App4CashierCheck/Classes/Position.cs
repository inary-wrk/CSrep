using System;

namespace App4CashierCheck.Classes
{
    public class Position
    {
        public  string name;
        public  uint numberOfThisItem;
        public decimal price;
        public decimal totalPrice;
               

        public string SetPosName()
        {
            Console.Write("Enter the amount of purchase: ");
            var _name = Console.ReadLine();
            return _name;
        }

        public decimal SetPrice()
        {
            decimal _price;
            string number;
            Console.Write("Enter the price of purchase: ");
            number = Console.ReadLine();

            while ((Decimal.TryParse(number, out _price) == false) || (_price < 0)) 
            {
                Console.Write("The price must be a positive number, please re - enter: ");
                number = Console.ReadLine();
            }
            return _price;
        }

        public uint SetNumberOfThisItem()
        {
            string number;
            uint _numberOfThisItem;
            Console.Write("Enter the number of this item: ");
            number = Console.ReadLine();

            while ((UInt32.TryParse(number, out _numberOfThisItem) == false) || (_numberOfThisItem < 1))
            {
                Console.Write("The number of this item must be a positive integer and greater than 0, please re - enter: ");
                number = Console.ReadLine();
            }
            return _numberOfThisItem;
        }

        public decimal SetPosTotalPrice()
        {
            return (numberOfThisItem * price);
        }
    }
}
