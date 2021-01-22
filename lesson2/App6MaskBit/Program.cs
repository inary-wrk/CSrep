// 6. (*) Для полного закрепления битовых масок, попытайтесь создать универсальную структуру расписания недели, к примеру,
// чтобы описать работу какого либо офиса. Явный пример - офис номер 1 работает со вторника до пятницы, офис номер 2 работает с понедельника до воскресенья.
using System;
using App6MaskBit.Classes;

namespace App6MaskBit
{
    class Program
    {
        static void Main(string[] args)
        {
            MaskWeekDays office1 = MaskWeekDays.Monday | MaskWeekDays.Wednesday | MaskWeekDays.Thursday | 
                                   MaskWeekDays.Friday | MaskWeekDays.Weekend;
            MaskWeekDays office2 = MaskWeekDays.Weekend | MaskWeekDays.Tuesday;

            DaysOfWeek dow = new DaysOfWeek();
            Console.WriteLine("Enter the number of the day of the week: ");
            var day = dow.WeekDaysCorrect(Console.ReadLine());
            var _day = (MaskWeekDays)Enum.GetValues(typeof(MaskWeekDays)).GetValue(day - 1);


            if ((_day & office1) == _day) Console.WriteLine("Today {0}, Office 1 is open", _day);
            if ((_day & office2) == _day) Console.WriteLine("Today {0}, Office 2 is open", _day);

            //switch (true)
            //{
            //    case true when (_day & office1) == _day:
            //        Console.WriteLine("Today {0}, Office 1 is open", _day);
            //        break;
            //    case true when (_day & office2) == _day:
            //        Console.WriteLine("Today {0}, Office 2 is open", _day);
            //        break;
            //}

            Console.ReadLine();
        }
    }
}
