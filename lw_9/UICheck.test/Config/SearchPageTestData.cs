namespace UICheck.test.Config;

public class SearchPageTestData
{
    public const string ValidSearchQuery = "ролл";
    
    public static readonly string[] ValidSearchQueries =
    {
        "ролл",
        "ролл Филадельфия",
        "ролл Калифорния",
        "суши",
        "нори",
        "тартар",
        "темпура",
        "Эби",
        "Тортилья",
        "Онигири",
        "Каша",
        "Ролл Филадельфия Лайт с огурцом"
    };
    
    public static readonly string[] InvalidSearchQueries =
    {
        "Наушники",
        "abcdef",
        "XXXXX12345",
        "пицца суши кит",
        "123456",
        "!@#$%^&*()",
        "йцукен",
        "     "
    };
}