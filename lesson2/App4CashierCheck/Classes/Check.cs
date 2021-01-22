using System;

namespace App4CashierCheck.Classes
{
    class Check
    {
        public uint checkNum;
        public decimal totalPrice;
        public DateTime currentTime;
        public Positions checkPositions;

        public Check (uint checkNum, Positions chekPosition,  decimal totalPrice, DateTime currentTime)
        {
            this.checkNum = checkNum;
            this.totalPrice = totalPrice;
            this.currentTime = currentTime;
            this.checkPositions = checkPositions;
        }

        public static uint GetCheckNum()
        {
            string number;
            uint _checkNum;
            Console.Write("Please enter the check number: ");
            number = Console.ReadLine();
            
            while (UInt32.TryParse(number, out _checkNum) == false)
            {
                Console.Write("The check number must be a positive integer, please re - enter: ");
                number = Console.ReadLine();

            }
            return _checkNum;
        }

      
    }
}
