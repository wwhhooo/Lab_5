namespace Lab_5
{
    class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Country(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString()
        {
            return $"ID Страны: {Id}, Название: {Name}";
        }
    }
}
