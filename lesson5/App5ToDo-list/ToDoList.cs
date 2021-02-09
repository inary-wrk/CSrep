/* 
5. (*) Список задач (ToDo-list):
- написать приложение для ввода списка задач;
- задачу описать классом ToDo с полями Title и IsDone;
- на старте, если есть файл tasks.json/xml/bin (выбрать формат), загрузить из него массив имеющихся задач и вывести их на экран;
- если задача выполнена, вывести перед её названием строку «[x]»;
- вывести порядковый номер для каждой задачи;
- при вводе пользователем порядкового номера задачи отметить задачу с этим порядковым номером как выполненную;
- записать актуальный массив задач в файл tasks.json/xml/bin.
*/

using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;

namespace App5ToDo_list
{
    class ToDo
    {
        public string Title { get; set; }
        public bool IsDone { get; set; }


        public ToDo(bool isdone, string title)
        {
            Title = title;
            IsDone = isdone;
        }


        public ToDo()
        {

        }
    }


    class ToDoList
    {
        private List<ToDo> taskList = new List<ToDo>();

        public List<ToDo> TaskList { get => taskList; set => taskList = value; }



        public void Create()
        {
            Console.WriteLine("An empty string will end input.");
            int i = 1;
            while (true)
            {
                Console.Write($"{i}. Enter task title: ");
                string title = Console.ReadLine();
                if (String.IsNullOrWhiteSpace(title)) return;
                TaskList.Add(new ToDo(false, title));
                i++;
            }
        }


        public void Show()
        {
            Console.Clear();
            foreach (var item in TaskList)
            {
                var x = item.IsDone == true ? "[x]" : String.Empty;
                Console.WriteLine($"{TaskList.IndexOf(item) + 1}. {x} {item.Title}");
            }
        }

        public void IsDone()
        {
            Console.WriteLine("An empty string will end input.");
            while (true)
            {
                Console.Write("Enter the number of the task for which you want to change the execution state: ");
                string number = Console.ReadLine();
                if (String.IsNullOrWhiteSpace(number)) return;
                if (!Int32.TryParse(number, out var intNumber) || intNumber > TaskList.Count || intNumber < 1)
                {
                    Console.WriteLine("The specified string is not an positive integer or does not match the task number.");
                    continue;
                }

                TaskList[intNumber - 1].IsDone = !TaskList[intNumber - 1].IsDone;
                var x = TaskList[intNumber - 1].IsDone == true ? "[x]" : String.Empty;
                Console.WriteLine("{0}. {1} {2}", intNumber, x, TaskList[intNumber - 1].Title);
            }
        }


        public static bool FileExist(string fileName)
        {
            foreach (var item in new List<string> { ".json", ".xml", ".bin" })
            {
                if (File.Exists(fileName) && (Path.GetExtension(fileName) == item)) return true;
            }
            return false;
        }
     

        public void SerializeJson(string file)
        {
            File.WriteAllText(file, JsonSerializer.Serialize(TaskList));
        }


        public List<ToDo> DeserializeJson(string file)
        {
            return JsonSerializer.Deserialize<List<ToDo>>(File.ReadAllText(file));
        }


        static void Main(string[] args)
        {
            string file = "tasks.json";
            var myList = new ToDoList();
            if (FileExist(file)) myList.TaskList = myList.DeserializeJson(file);
            else myList.Create();
            myList.Show();
            myList.IsDone();
            myList.Show();
            myList.SerializeJson(file);
        }
    }
}
