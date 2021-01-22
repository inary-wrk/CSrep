using System;

namespace App4CashierCheck.Classes
{
    class Magazine
    {
        public string name;
        public string cashier;
        public LegalData data;

        public Magazine()
        {
            name = SetMagazineName();
            cashier = SetCahierName();
            data = SetLegalData();
        }

        public string SetMagazineName()
        {
            Console.Write("Enter magazine Name: ");
            string name = Console.ReadLine();
            return name;
        }

        public string SetCahierName()
        {
            Console.Write("Enter chaier Name: ");
            string name = Console.ReadLine();
            return name;
        }

        public LegalData SetLegalData()
        {
            var data = new LegalData();

            return data;
        }
    }
}
