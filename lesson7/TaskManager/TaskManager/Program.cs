/* Написать консольное приложение Task Manager, которое выводит на экран запущенные процессы и позволяет завершить указанный процесс.
 * Предусмотреть возможность завершения процессов с помощью указания его ID или имени процесса. В качестве примера можно использовать
 * консольные утилиты Windows tasklist и taskkill.*/

using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace TaskManager
{
    class Program
    {

        static void ShowHelp()
        {
            Console.WriteLine("-l (show process list)\n" +
                              "-kid \"id\" (terminate the process by id)\n" +
                              "-kn \"name\" (terminate the process by name)\n" +
                              "-help (show help)");
        }


        static void ShowProcesses()
        {
            int idSize = 10;
            int nameSize = 0;
            int top = Console.CursorTop + 2;
            var arrProcess = Process.GetProcesses();

            foreach (var item in arrProcess)
            {
                if (item.ProcessName.Length > nameSize) nameSize = item.ProcessName.Length;
            }
            nameSize = nameSize + 4;

            Console.SetCursorPosition(0, top - 1);
            Console.Write("Process Name");
            Console.SetCursorPosition(nameSize, top - 1);
            Console.Write("PID");
            Console.SetCursorPosition(nameSize + idSize, top - 1);
            Console.Write("Memory");

            foreach (var item in arrProcess)
            {
                Console.SetCursorPosition(0, top);
                Console.Write($"|{item.ProcessName}");
                Console.SetCursorPosition(nameSize, top);
                Console.Write($"|{item.Id}");
                Console.SetCursorPosition(nameSize + idSize, top);
                Console.Write($"|{item.WorkingSet64 >> 20 } Mb");
                top++;
            }
        }


        static void KillProcess(int id)
        {
            foreach (var item in Process.GetProcesses())
            {
                if (item.Id == id)
                {
                    try
                    {
                        item.Kill();
                        item.WaitForExit();
                        Console.WriteLine($"Process: {item.ProcessName} id: {item.Id} was terminate successfully");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    return;
                }
            }
            Console.WriteLine("Process with id: {0} not found", id);
        }


        static void KillProcess(string name)
        {
            var listProc = new List<Process>();
            foreach (var item in Process.GetProcesses())
            {
                if (item.ProcessName == name)
                {
                    listProc.Add(item);
                    Console.WriteLine($"Name: {item.ProcessName} id: {item.Id}");
                }
            }
            switch (listProc.Count)
            {
                case 0: Console.WriteLine($"The process with the specified name: {name} not found"); break;
                case 1:
                    try
                    {
                        listProc[0].Kill();
                        listProc[0].WaitForExit();
                        Console.WriteLine($"Process was terminate successfully");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    break;
                default:
                    Console.WriteLine("More than one process found with {0} x.", name);
                    Console.WriteLine("Do you want to terminate all processes? Press Y (yes) or any key (no)");
                    if (Console.ReadKey(true).Key != ConsoleKey.Y) return;
                    foreach (var item in listProc)
                    {
                        try
                        {
                            item.Kill();
                            item.WaitForExit();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                    Console.WriteLine("All processes have been terminate successfully");
                    break;
            }
        }


        static int Main(string[] args)
        {
            if (args.Length == 0)
            {
                ShowHelp();
                return 0;
            }
            switch (args[0])
            {
                case "-l": ShowProcesses(); break;
                case "-kid" when (args.Length > 1) && (Int32.TryParse(args[1], out var id)): KillProcess(id); break;
                case "-kn" when args.Length > 1: KillProcess(args[1]); break;
                case "-help": ShowHelp(); break;
                default:
                    ShowHelp();
                    break;
            }
            return 0;
        }
    }
}
