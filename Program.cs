using System;

namespace Lab_5
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите путь к Excel файлу:");
            string filePath = Console.ReadLine();

            Database database = new Database();
            database.LoadFromExcel(filePath);

            Console.WriteLine("Выберите действие:");
            Console.WriteLine("1. Просмотр базы данных");
            Console.WriteLine("2. Удаление элемента");
            Console.WriteLine("3. Корректировка элемента");
            Console.WriteLine("4. Добавление элемента");
            Console.WriteLine("5. Выполнение запросов");
            Console.WriteLine("6. Выход");

            string choice = Console.ReadLine();
            while (choice != "6")
            {
                switch (choice)
                {
                    case "1":
                        database.ViewDatabase();
                        break;
                    case "2":
                        database.DeleteElement();
                        break;
                    case "3":
                        database.UpdateElement();
                        break;
                    case "4":
                        database.AddElement();
                        break;
                    case "5":
                        database.ExecuteQueries();
                        break;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        break;
                }

                Console.WriteLine("Выберите действие:");
                choice = Console.ReadLine();
            }
        }
    }
}
