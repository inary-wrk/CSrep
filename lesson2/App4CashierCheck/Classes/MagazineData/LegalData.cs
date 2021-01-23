using System;

namespace App4CashierCheck.Classes
{

    public class LegalData
    {
        public ulong kKM;
        public ulong iNN;
        public uint numEKLZ;
        public string idEKLZ;

        public LegalData()
        {
            kKM = SetKKM();
            iNN = SetINN();
            numEKLZ = SetNumEKLZ();
            idEKLZ = SetIdEKLZ();
        }

        public ulong SetKKM()
        {
            Console.Write("Enter the KKM: ");
            string _kKM = Console.ReadLine();
            ulong kKM;
            while ((ulong.TryParse(_kKM, out kKM) == false) || (kKM < 0))
            {
                Console.Write("The KKM must be a positive integer, please re - enter: ");
                _kKM = Console.ReadLine();
            }
            return kKM;
        }

        public ulong SetINN()
        {
            Console.Write("Enter the INN: ");
            string _iNN = Console.ReadLine();
            ulong iNN;
            while ((ulong.TryParse(_iNN, out iNN) == false) || (iNN < 0) || (_iNN.Length != 12))
            {
                Console.Write("The INN must be a positive integer and consist of 12 digits, please re - enter: ");
                _iNN = Console.ReadLine();
            }
            return iNN;
        }

        public uint SetNumEKLZ()
        {
            Console.Write("Enter the EKLZ number: ");
            string numEKLZ = Console.ReadLine();
            uint number;

            while ((UInt32.TryParse(numEKLZ, out number) == false) || (number < 0))
            {
                Console.Write("EKLZ number must be a positive integer, please re - enter: ");
                numEKLZ = Console.ReadLine();
            }
            return number;
        }

        public string SetIdEKLZ()
        {
            Console.Write("Enter EKLZ id: ");
            return Console.ReadLine();
        }
    }
}
