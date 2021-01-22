using System;
using System.Collections.Generic;

namespace App4CashierCheck.Classes
{
    class Check
    {
        public uint checkNum;
        public uint numOfPos;
        public List<Position> checkPositions;
        public decimal checkTotalPrice;
        public DateTime currentTime;

        public Check()
        {
            checkNum = SetCheckNum();
            numOfPos = SetNumOfPositions();
            checkPositions = SetPositions();
            checkTotalPrice = CheckTotalPrice();
            currentTime = DateTime.Now;

        }

        
        public uint SetCheckNum()
        {
            string number;
            uint _checkNum;
            Console.WriteLine("Enter the check number: ");
            number = Console.ReadLine();
            
            while (UInt32.TryParse(number, out _checkNum) == false || (_checkNum < 1))
            {
                Console.Write("The check number must be a positive integer and greater than 0, please re - enter: ");
                number = Console.ReadLine();

            }
            return _checkNum;
        }

        public uint SetNumOfPositions()
        {
            string number;
            uint _numOfPos;
            Console.Write("Enter the number of positions: ");
            number = Console.ReadLine();

            while ((UInt32.TryParse(number, out _numOfPos) == false) || (_numOfPos< 1))
            {
                Console.Write("The amount of position must be a positive integer and greater than 0, please re - enter: ");
                number = Console.ReadLine();
            }
            return _numOfPos;
        }

        public List<Position> SetPositions()
        {
            
            var currentPositions = new List<Position>(Convert.ToInt32(numOfPos));
            foreach (var item in currentPositions)
            {
                item.name = item.SetPosName();
                item.numberOfThisItem = item.SetNumberOfThisItem();
                item.price = item.SetPrice();
                item.totalPrice = item.SetPosTotalPrice();
            }

            return currentPositions;
        }

        public decimal CheckTotalPrice()
        {
            decimal _totalPrice = 0;
            foreach (var item in checkPositions)
            {
                _totalPrice += (item.numberOfThisItem * item.price);
            }
            return _totalPrice;
        }

    }
}
