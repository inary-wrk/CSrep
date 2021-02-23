using System;
using System.Collections.Generic;

namespace App4CashierCheck.Classes
{
    class Receipt
    {
        public uint number;
        public uint numberOfProducts;
        public List<Product> products;
        public decimal receiptTotalPrice;
        public DateTime currentTime;

        public Receipt()
        {
            number = SetReceiptNumber();
            products = SetProduct();
            receiptTotalPrice = CalculateTotalPrice();
            currentTime = DateTime.Now;
        }


        public uint SetReceiptNumber()
        {
            string number;
            uint receiptNum;
            Console.WriteLine("Enter the sales receipt number: ");
            number = Console.ReadLine();

            while (UInt32.TryParse(number, out receiptNum) == false || (receiptNum < 1))
            {
                Console.Write("The sales receipt number must be a positive integer, please re - enter: ");
                number = Console.ReadLine();
            }
            return receiptNum;
        }


        public List<Product> SetProduct()
        {
            List<Product> goods = new List<Product>();
            uint i = 0;
            while (true)
            {
                ++i;
                Product item = new Product();
                goods.Add(item);
                Console.WriteLine($"№ {i}. Name: {item.name}, quantity of goods: {item.quantity}, price: {item.price}, total price: {item.totalPrice}");

                Console.WriteLine("Do you want to continue? (y/n)");
                if (!Equals("y", Console.ReadLine().ToLower())) { numberOfProducts = i;  break; }
            }
            return goods;
        }

        public decimal CalculateTotalPrice()
        {
            decimal _totalPrice = 0;
            foreach (Product item in products)
                _totalPrice += (item.quantity * item.price);
            return _totalPrice;
        }

    }
}
