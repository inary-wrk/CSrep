using System;

namespace App4CashierCheck.Classes
{
    public class Product
    {
        public string name;
        public uint quantity;
        public decimal price;
        public decimal totalPrice;

        public Product()
        {
            name = SetProductName();
            quantity = SetQuantity();
            price = SetPrice();
            totalPrice = SetProductTotalPrice();
        }
        

        public string SetProductName()
        {
            Console.Write("Enter product name: ");
            return Console.ReadLine();
        }

        public decimal SetPrice()
        {
            decimal _price;
            string number;
            Console.Write("Enter product price: ");
            number = Console.ReadLine();

            while ((Decimal.TryParse(number, out _price) == false) || (_price < 0)) 
            {
                Console.Write("Product price must be a positive number, please re - enter: ");
                number = Console.ReadLine();
            }
            return _price;
        }

        public uint SetQuantity()
        {
            string number;
            uint quantity;
            Console.Write("Enter product quantity: ");
            number = Console.ReadLine();

            while ((UInt32.TryParse(number, out quantity) == false) || (quantity < 1))
            {
                Console.Write("Product quantity must be a positive integer, please re - enter: ");
                number = Console.ReadLine();
            }
            return quantity;
        }

        public decimal SetProductTotalPrice()
        {
            return (quantity * price);
        }
    }
}
