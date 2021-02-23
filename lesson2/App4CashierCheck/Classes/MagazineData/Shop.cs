using System;

namespace App4CashierCheck.Classes
{
    class Shop
    {
        public string shopName;
        public string cashierName;
        public LegalData data;

        public Shop()
        {
            shopName = SetShopName();
            cashierName = SetCashierName();
            data = SetLegalData();
        }

        public string SetShopName()
        {
            Console.Write("Enter shop Name: ");
            return Console.ReadLine();
        }

        public string SetCashierName()
        {
            Console.Write("Enter cashier Name: ");
            return Console.ReadLine();
        }

        public LegalData SetLegalData()
        {
            return new LegalData();
        }
    }
}
