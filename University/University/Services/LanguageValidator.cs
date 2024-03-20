namespace University.Services
{
    public static class LanguageValidator
    {
        public static void ValidateWordEnUa(string word)
        {
            Regex englishWordPattern = new("^[a-zA-Z -]+$");
            Regex ukrainianWordPattern = new("^[АаБбВвГгҐґДдЕеЄєЖжЗзИиІіЇїЙйКкЛлМмНнОоПпРрСсТтУуФфХхЦцЧчШшЩщьЮюЯя -]+$");

            if (!englishWordPattern.IsMatch(word))
            {
                if (!ukrainianWordPattern.IsMatch(word))
                    throw new ArgumentException(nameof(word), "Parameter must consist of english or ukrainian letters only");
            }
        }
    }
}
