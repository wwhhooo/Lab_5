namespace Lab_5
{
    class Achievement
    {
        public int ClubId { get; set; }
        public int Gold { get; set; }
        public int Silver { get; set; }
        public int Bronze { get; set; }
        public int Cup { get; set; }
        public int CupFinal { get; set; }
        public int ChampionsLeague { get; set; }
        public int ChampionsLeagueFinal { get; set; }
        public int EuropaLeague { get; set; }
        public int EuropaLeagueFinal { get; set; }
        public int CupWinnersCup { get; set; }
        public int CupWinnersCupFinal { get; set; }
        public int ConferenceLeague { get; set; }
        public int ConferenceLeagueFinal { get; set; }

        public Achievement(int clubId, int gold, int silver, int bronze, int cup, int cupFinal, int championsLeague, int championsLeagueFinal, int europaLeague, int europaLeagueFinal, int cupWinnersCup, int cupWinnersCupFinal, int conferenceLeague, int conferenceLeagueFinal)
        {
            ClubId = clubId;
            Gold = gold;
            Silver = silver;
            Bronze = bronze;
            Cup = cup;
            CupFinal = cupFinal;
            ChampionsLeague = championsLeague;
            ChampionsLeagueFinal = championsLeagueFinal;
            EuropaLeague = europaLeague;
            EuropaLeagueFinal = europaLeagueFinal;
            CupWinnersCup = cupWinnersCup;
            CupWinnersCupFinal = cupWinnersCupFinal;
            ConferenceLeague = conferenceLeague;
            ConferenceLeagueFinal = conferenceLeagueFinal;
        }

        public override string ToString()
        {
            return $"Club ID: {ClubId}, Gold: {Gold}, Silver: {Silver}, Bronze: {Bronze}, Cup: {Cup}, CupFinal: {CupFinal}, ChampionsLeague: {ChampionsLeague}, ChampionsLeagueFinal: {ChampionsLeagueFinal}, EuropaLeague: {EuropaLeague}, EuropaLeagueFinal: {EuropaLeagueFinal}, CupWinnersCup: {CupWinnersCup}, CupWinnersCupFinal: {CupWinnersCupFinal}, ConferenceLeague: {ConferenceLeague}, ConferenceLeagueFinal: {ConferenceLeagueFinal}";
        }
    }
}
