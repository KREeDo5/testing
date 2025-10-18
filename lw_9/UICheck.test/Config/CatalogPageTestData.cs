namespace UICheck.test.Config;

public class CatalogPageTestData
{
    public const string CatalogUrl = "https://prostayaeda.ru/catalog/";
    
    public static readonly string[] SortOptions =
    {
        "По умолчанию",          // default
        "От новых к старым",     // created
        "По возрастанию цены",   // price_asc
        "По убыванию цены",      // price_desc
        "По популярности"        // rate
    };
}