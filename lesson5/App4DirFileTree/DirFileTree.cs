// 4. Сохранить дерево каталогов и файлов по заданному пути в текстовый файл — с рекурсией и без.

using System;
using System.IO;
using System.Collections.Generic;

namespace App4DirFileTree
{
    class DirFileTree
    {
        private static List<string> logTreeRecurs = new List<string>(1) { "Directory tree recursive log" };
        private static List<string> treeRec = new List<string>();

        static List<string> LogTreeRecursion { get => logTreeRecurs; set => logTreeRecurs = value; }
        static List<string> TreeRec { get => treeRec; set => treeRec = value; }



        static void TreeRecurs(DirectoryInfo root)
        {
            if (root is null) { Console.WriteLine("Directory path is null."); return; }
            FileInfo[] files = new FileInfo[0];
            DirectoryInfo[] subDir = new DirectoryInfo[0];
            LogTreeRecursion.Add(root.FullName);

            try
            {
                subDir = root.GetDirectories();
                LogTreeRecursion.Add("Directories received without exception.");
            }
            catch (Exception ex)
            {
                LogTreeRecursion.Add(ex.Message);
            }

            try
            {
                files = root.GetFiles();
                LogTreeRecursion.Add("Files received without exception.");
            }
            catch (Exception ex)
            {
                LogTreeRecursion.Add(ex.Message);
            }



            foreach (var dir in subDir)
            {
                TreeRec.Add($"{dir.FullName} (directory)");
                TreeRecurs(dir);
            }
            foreach (var file in files)
            {
                TreeRec.Add($"{file.FullName} (file)");
            }

        }

        static List<string> TreeLoop(DirectoryInfo root, out List<string> log)
        {
            log = new List<string>(1) { "Directory tree loop log" };
            List<string> tree = new List<string>();
            List<DirectoryInfo> listDirs = new List<DirectoryInfo>();
            if (root is null) { Console.WriteLine("Directory path is null."); return tree; }
            listDirs.Add(root);

            while (listDirs.Count > 0)
            {
                FileInfo[] files = new FileInfo[0];
                DirectoryInfo[] subDir = new DirectoryInfo[0];
                log.Add(listDirs[0].FullName);

                try
                {
                    subDir = listDirs[0].GetDirectories();
                    log.Add("Directories received without exception.");
                }
                catch (Exception ex)
                {
                    log.Add(ex.Message);
                }

                try
                {
                    files = listDirs[0].GetFiles();
                    log.Add("Files received without exception.");
                }
                catch (Exception ex)
                {
                    log.Add(ex.Message);
                }


                foreach (var dir in subDir)
                {
                    tree.Add($"{dir.FullName} (directory)");
                    listDirs.Add(dir);
                }
                foreach (var file in files)
                {
                    tree.Add($"{file.FullName} (file)");
                }
                listDirs.RemoveAt(0);
            }
            return tree;
        }


        static DirectoryInfo EnterRootDir()
        {
            Console.WriteLine("Enter the path to the root folder.");
            string path = Console.ReadLine();
            if (!Directory.Exists(path))
            {
                Console.WriteLine("The specified directory was not found.");
                return null;
            }
            try
            {
                return new DirectoryInfo(path);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }


        static void ShowLogs(List<string> log)
        {
            if (log.Count > 1)
            {
                Console.WriteLine($"Show {log[0]}? Press Y (yes) or any key (no).");
                if (Console.ReadKey(true).Key == ConsoleKey.Y)
                {
                    foreach (var item in log)
                    {
                        Console.WriteLine(item);
                    }
                }
            }
        }


        static void Main(string[] args)
        {
            var myRootDir = EnterRootDir();
            TreeRecurs(myRootDir);
            var myTreeLoop = TreeLoop(myRootDir, out var logMyTreeLoop);

            File.WriteAllLines("TreeDirectories_recursion.txt", TreeRec);
            File.WriteAllLines("TreeDirectories_recursion.log", LogTreeRecursion);

            File.WriteAllLines("TreeDirectories_loop.txt", myTreeLoop);
            File.WriteAllLines("TreeDirectories_loop.log", logMyTreeLoop);
            ShowLogs(LogTreeRecursion);
            ShowLogs(logMyTreeLoop);
            Console.ReadKey();
        }
    }
}

