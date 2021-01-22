using System;

namespace App4CashierCheck.Classes
{
    class Magazine
    {
        public string name;
        public string cashier;
        public LegalData data;

        public Magazine(string name, string cashier, LegalData data)
        {
            this.name = name;
            this.cashier = cashier;
            this.data = data;
        }
    }
}
