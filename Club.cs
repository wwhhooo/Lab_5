using System.Collections.Generic;

namespace Lab_5
{
    class Club
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }
        public List<Achievement> Achievements { get; set; }

        public Club(int id, string name, int countryId)
        {
            Id = id;
            Name = name;
            CountryId = countryId;
            Achievements = new List<Achievement>();
        }

        public override string ToString()
        {
            return $"ID клуба: {Id}, Название: {Name}, ID Страны: {CountryId}";
        }
    }
}
