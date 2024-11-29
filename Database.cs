using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Aspose.Cells;

namespace Lab_5
{
    class Database
    {
        private List<Country> countries;
        private List<Club> clubs;
        private List<Achievement> achievements;
        private StreamWriter LogWriter;
        private StreamWriter LogActions;

        public Database() // Инициализирует списки для хранения данных и создает лог-файл.
        {
            countries = new List<Country>();
            clubs = new List<Club>();
            achievements = new List<Achievement>();

            Console.WriteLine("Создать новый лог файл для отслеживания действий? (y/n)");
            string createNewLog = Console.ReadLine();
            if (createNewLog.ToLower() == "y")
            {
                LogActions = new StreamWriter("action_log.txt", false);
            }
            else
            {
                LogActions = new StreamWriter("action_log.txt", true);
            }

            Console.WriteLine("Создать новый лог файл для безопасного чтения из файла? (y/n)");
            string createNewReadLog = Console.ReadLine();
            if (createNewReadLog.ToLower() == "y")
            {
                LogWriter = new StreamWriter("read_log.txt", false);
            }
            else
            {
                LogWriter = new StreamWriter("read_log.txt", true);
            }
        }

        public void LoadFromExcel(string filePath) // Загружает данные из Excel файла в соответствующие списки.
        {
            Workbook wb;
            try
            {
                wb = new Workbook(filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при чтении Excel файла: " + ex.Message);
                LogActions.WriteLine("Ошибка при чтении Excel файла: " + ex.Message);
                return;
            }

            WorksheetCollection sheets = wb.Worksheets;

            for (int i = 0; i < sheets.Count; i++)
            {
                Worksheet ws = sheets[i];
                Console.WriteLine(ws.Name);
                Cells rows = ws.Cells;

                switch (ws.Name)
                {
                    case "Страны":
                        for (int row = 1; row <= rows.MaxDataRow; row++)
                        {
                            int id = GetIntValue(rows[row, 0]);
                            string name = GetStringValue(rows[row, 1]);
                            if (string.IsNullOrEmpty(name))
                            {
                                Console.WriteLine($"Пустая ячейка в строке {row}, столбце 1 листа 'Страны'.");
                                continue;
                            }
                            countries.Add(new Country(id, name));
                        }
                        break;
                    case "Клубы":
                        for (int row = 1; row <= rows.MaxDataRow; row++)
                        {
                            int id = GetIntValue(rows[row, 0]);
                            string name = GetStringValue(rows[row, 1]);
                            int countryId = GetIntValue(rows[row, 2]);
                            if (string.IsNullOrEmpty(name))
                            {
                                Console.WriteLine($"Пустая ячейка в строке {row}, столбце 1 листа 'Клубы'.");
                                continue;
                            }
                            clubs.Add(new Club(id, name, countryId));
                        }
                        break;
                    case "Достижения":
                        for (int row = 1; row <= rows.MaxDataRow; row++)
                        {
                            int clubId = GetIntValue(rows[row, 0]);
                            int gold = GetIntValue(rows[row, 1]);
                            int silver = GetIntValue(rows[row, 2]);
                            int bronze = GetIntValue(rows[row, 3]);
                            int cup = GetIntValue(rows[row, 4]);
                            int cupFinal = GetIntValue(rows[row, 5]);
                            int championsLeague = GetIntValue(rows[row, 6]);
                            int championsLeagueFinal = GetIntValue(rows[row, 7]);
                            int europaLeague = GetIntValue(rows[row, 8]);
                            int europaLeagueFinal = GetIntValue(rows[row, 9]);
                            int cupWinnersCup = GetIntValue(rows[row, 10]);
                            int cupWinnersCupFinal = GetIntValue(rows[row, 11]);
                            int conferenceLeague = GetIntValue(rows[row, 12]);
                            int conferenceLeagueFinal = GetIntValue(rows[row, 13]);
                            achievements.Add(new Achievement(clubId, gold, silver, bronze, cup, cupFinal, championsLeague, championsLeagueFinal, europaLeague, europaLeagueFinal, cupWinnersCup, cupWinnersCupFinal, conferenceLeague, conferenceLeagueFinal));
                        }
                        break;
                    default:
                        Console.WriteLine($"Лист {ws.Name} не распознан.");
                        break;
                }
            }

            LogActions.WriteLine("Данные успешно загружены из Excel файла.");
        }

        // Методы для безопасного чтения значений из ячеек Excel.
        private int GetIntValue(Cell cell)
        {
            if (cell.Value == null)
            {
                LogWriter.WriteLine($"Пустая ячейка: {cell.Name}");
                return 0;
            }
            return cell.IntValue;
        }

        private string GetStringValue(Cell cell)
        {
            if (cell.Value == null)
            {
                LogWriter.WriteLine($"Пустая ячейка: {cell.Name}");
                return string.Empty;
            }
            return cell.StringValue;
        }

        // Метод для просмотра всх данных в базе данных.
        public void ViewDatabase()
        {
            Console.WriteLine("Страны:");
            foreach (var country in countries)
            {
                Console.WriteLine(country);
            }

            Console.WriteLine("Клубы:");
            foreach (var club in clubs)
            {
                Console.WriteLine(club);
            }

            Console.WriteLine("Достижения:");
            foreach (var achievement in achievements)
            {
                Console.WriteLine(achievement);
            }

            LogActions.WriteLine("Просмотр базы данных.");
        }

        // Метод для удаления элемента из таблицы по ID.
        public void DeleteElement()
        {
            Console.WriteLine("Выберите таблицу для удаления элемента:");
            Console.WriteLine("1. Страны");
            Console.WriteLine("2. Клубы");
            Console.WriteLine("3. Достижения");
            string tableChoice = Console.ReadLine();

            Console.WriteLine("Введите ID элемента для удаления:");
            int id = int.Parse(Console.ReadLine());

            switch (tableChoice)
            {
                case "1":
                    DeleteCountry(id);
                    break;
                case "2":
                    DeleteClub(id);
                    break;
                case "3":
                    DeleteAchievement(id);
                    break;
                default:
                    Console.WriteLine("Неверный выбор таблицы.");
                    break;
            }
        }

        private void DeleteCountry(int id)
        {
            var country = countries.FirstOrDefault(c => c.Id == id);
            if (country != null)
            {
                countries.Remove(country);
                Console.WriteLine("Страна удалена.");
                LogActions.WriteLine($"Страна с ID {id} удалена.");
            }
            else
            {
                Console.WriteLine("Страна не найдена.");
                LogActions.WriteLine($"Попытка удалить страну с ID {id} не удалась.");
            }
        }

        private void DeleteClub(int id)
        {
            var club = clubs.FirstOrDefault(c => c.Id == id);
            if (club != null)
            {
                clubs.Remove(club);
                Console.WriteLine("Клуб удален.");
                LogActions.WriteLine($"Клуб с ID {id} удален.");
            }
            else
            {
                Console.WriteLine("Клуб не найден.");
                LogActions.WriteLine($"Попытка удалить клуб с ID {id} не удалась.");
            }
        }

        private void DeleteAchievement(int id)
        {
            var achievement = achievements.FirstOrDefault(a => a.ClubId == id);
            if (achievement != null)
            {
                achievements.Remove(achievement);
                Console.WriteLine("Достижение удалено.");
                LogActions.WriteLine($"Достижение с Club ID {id} удалено.");
            }
            else
            {
                Console.WriteLine("Достижение не найдено.");
                LogActions.WriteLine($"Попытка удалить достижение с Club ID {id} не удалась.");
            }
        }

        public void UpdateElement()
        {
            Console.WriteLine("Выберите таблицу для корректировки элемента:");
            Console.WriteLine("1. Страны");
            Console.WriteLine("2. Клубы");
            Console.WriteLine("3. Достижения");
            string tableChoice = Console.ReadLine();

            Console.WriteLine("Введите ID элемента для корректировки:");
            int id = int.Parse(Console.ReadLine());

            switch (tableChoice)
            {
                case "1":
                    UpdateCountry(id);
                    break;
                case "2":
                    UpdateClub(id);
                    break;
                case "3":
                    UpdateAchievement(id);
                    break;
                default:
                    Console.WriteLine("Неверный выбор таблицы.");
                    break;
            }
        }

        private void UpdateCountry(int id)
        {
            var country = countries.FirstOrDefault(c => c.Id == id);
            if (country != null)
            {
                Console.WriteLine("Введите новое имя страны:");
                string newName = Console.ReadLine();
                country.Name = newName;
                Console.WriteLine("Страна обновлена.");
                LogActions.WriteLine($"Страна с ID {id} обновлена.");
            }
            else
            {
                Console.WriteLine("Страна не найдена.");
                LogActions.WriteLine($"Попытка обновить страну с ID {id} не удалась.");
            }
        }

        private void UpdateClub(int id)
        {
            var club = clubs.FirstOrDefault(c => c.Id == id);
            if (club != null)
            {
                Console.WriteLine("Введите новое имя клуба:");
                string newName = Console.ReadLine();
                club.Name = newName;
                Console.WriteLine("Клуб обновлен.");
                LogActions.WriteLine($"Клуб с ID {id} обновлен.");
            }
            else
            {
                Console.WriteLine("Клуб не найден.");
                LogActions.WriteLine($"Попытка обновить клуб с ID {id} не удалась.");
            }
        }

        private void UpdateAchievement(int id)
        {
            var achievement = achievements.FirstOrDefault(a => a.ClubId == id);
            if (achievement != null)
            {
                Console.WriteLine("Введите новое количество золотых медалей:");
                int newGold = int.Parse(Console.ReadLine());
                achievement.Gold = newGold;
                Console.WriteLine("Достижение обновлено.");
                LogActions.WriteLine($"Достижение с Club ID {id} обновлено.");
            }
            else
            {
                Console.WriteLine("Достижение не найдено.");
                LogActions.WriteLine($"Попытка обновить достижение с Club ID {id} не удалась.");
            }
        }

        public void AddElement()
        {
            Console.WriteLine("Выберите таблицу для добавления элемента:");
            Console.WriteLine("1. Страны");
            Console.WriteLine("2. Клубы");
            Console.WriteLine("3. Достижения");
            string tableChoice = Console.ReadLine();

            switch (tableChoice)
            {
                case "1":
                    AddCountry();
                    break;
                case "2":
                    AddClub();
                    break;
                case "3":
                    AddAchievement();
                    break;
                default:
                    Console.WriteLine("Неверный выбор таблицы.");
                    break;
            }
        }

        private void AddCountry()
        {
            Console.WriteLine("Введите ID новой страны:");
            int countryId = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите имя новой страны:");
            string countryName = Console.ReadLine();
            countries.Add(new Country(countryId, countryName));
            Console.WriteLine("Страна добавлена.");
            LogActions.WriteLine($"Страна с ID {countryId} добавлена.");
        }

        private void AddClub()
        {
            Console.WriteLine("Введите ID нового клуба:");
            int clubId = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите имя нового клуба:");
            string clubName = Console.ReadLine();
            Console.WriteLine("Введите ID страны:");
            int clubCountryId = int.Parse(Console.ReadLine());
            clubs.Add(new Club(clubId, clubName, clubCountryId));
            Console.WriteLine("Клуб добавлен.");
            LogActions.WriteLine($"Клуб с ID {clubId} добавлен.");
        }

        private void AddAchievement()
        {
            Console.WriteLine("Введите Club ID нового достижения:");
            int achievementClubId = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите количество золотых медалей:");
            int gold = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите количество серебряных медалей:");
            int silver = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите количество бронзовых медалей:");
            int bronze = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите количество кубков:");
            int cup = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите количество финалов кубков:");
            int cupFinal = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите количество побед в Лиге Чемпионов:");
            int championsLeague = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите количество финалов Лиги Чемпионов:");
            int championsLeagueFinal = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите количество побед в Лиге Европы:");
            int europaLeague = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите количество финалов Лиги Европы:");
            int europaLeagueFinal = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите количество побед в Кубке обладателей кубков:");
            int cupWinnersCup = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите количество финалов Кубка обладателей кубков:");
            int cupWinnersCupFinal = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите количество побед в Лиге Конференций:");
            int conferenceLeague = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите количество финалов Лиги Конференций:");
            int conferenceLeagueFinal = int.Parse(Console.ReadLine());
            achievements.Add(new Achievement(achievementClubId, gold, silver, bronze, cup, cupFinal, championsLeague, championsLeagueFinal, europaLeague, europaLeagueFinal, cupWinnersCup, cupWinnersCupFinal, conferenceLeague, conferenceLeagueFinal));
            Console.WriteLine("Достижение добавлено.");
            LogActions.WriteLine($"Достижение с Club ID {achievementClubId} добавлено.");
        }

        public void ExecuteQueries()
        {
            // Пример запроса 1: Клубы, которые побеждали в чемпионате страны, но ни разу не выиграли национальный кубок
            var query1 = from club in clubs
                         join achievement in achievements on club.Id equals achievement.ClubId
                         where achievement.Gold > 0 && achievement.Cup == 0
                         select club;

            var maxWinsClub = query1.OrderByDescending(c => c.Achievements.Sum(a => a.Gold)).FirstOrDefault();
            if (maxWinsClub != null)
            {
                Console.WriteLine($"ID страны: {maxWinsClub.CountryId}");
                LogActions.WriteLine($"Запрос 1: ID страны: {maxWinsClub.CountryId}");
            }
            else
            {
                Console.WriteLine("Нет клубов, соответствующих условиям запроса.");
                LogActions.WriteLine("Запрос 1: Нет клубов, соответствующих условиям запроса.");
            }

            // Пример запроса 2: Клубы, которые выиграли национальный кубок и Лигу Чемпионов
            var query2 = from club in clubs
                          join achievement in achievements on club.Id equals achievement.ClubId
                          where achievement.Cup > 0 && achievement.ChampionsLeague > 0
                          select club;

            Console.WriteLine("Клубы, которые выиграли национальный кубок и Лигу Чемпионов:");
            foreach (var club in query2)
            {
                Console.WriteLine(club);
            }
            LogActions.WriteLine("Запрос 2: Клубы, которые выиграли национальный кубок и Лигу Чемпионов.");

            // Пример запроса 3: Клубы, которые выиграли национальный кубок, Лигу Чемпионов и Лигу Европы
            var query3 = from club in clubs
                          join achievement in achievements on club.Id equals achievement.ClubId
                          where achievement.Cup > 0 && achievement.ChampionsLeague > 0 && achievement.EuropaLeague > 0
                          select club;

            Console.WriteLine("Клубы, которые выиграли национальный кубок, Лигу Чемпионов и Лигу Европы:");
            foreach (var club in query3)
            {
                Console.WriteLine(club);
            }
            LogActions.WriteLine("Запрос 3: Клубы, которые выиграли национальный кубок, Лигу Чемпионов и Лигу Европы.");

            // Пример запроса 4: Клуб с наибольшим количеством побед в Лиге Чемпионов
            var query4 = (from club in clubs
                           join achievement in achievements on club.Id equals achievement.ClubId
                           orderby achievement.ChampionsLeague descending
                           select club).FirstOrDefault();

            if (query4 != null)
            {
                Console.WriteLine($"Клуб с наибольшим количеством побед в Лиге Чемпионов: {query4.Name}");
                LogActions.WriteLine($"Запрос 4: Клуб с наибольшим количеством побед в Лиге Чемпионов: {query4.Name}");
            }
            else
            {
                Console.WriteLine("Нет клубов, соответствующих условиям запроса.");
                LogActions.WriteLine("Запрос 4: Нет клубов, соответствующих условиям запроса.");
            }
        }

        /*
        ~Database()
        {
            LogActions.Close();
            LogWriter.Close();
        }*/
    }
}
