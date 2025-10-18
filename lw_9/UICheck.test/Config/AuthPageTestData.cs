namespace UICheck.test.Config;

public class AuthPageTestData
{   
    public const string PersonalPageUrl = "https://prostayaeda.ru/personal/";
    
    public static readonly (string Login, string Password)[] InvalidLoginCredentials =
    {
        ("", ""), // оба поля пустые
        ("799611631", "passwd"), // неправильный формат телефона, короткий номер
        ("abcdefghij", "passwd123"), // буквенный логин
        ("79961163172", ""), // валидный номер, пустой пароль
        ("", "wrongpass123"), // пустой логин, не пустой пароль
        ("1111111111", "wrongpass"), // несуществующий номер, левый пароль
        ("+7 (999) 123-45-67", "пароль"), // международный формат, кириллица в пароле
        ("123456789012345", "Pass123!"), // слишком длинный логин
        ("admin", "admin") // попытка логина под админом
    };


    public const string InvalidLogin = "79999999933";
    public const string ExistingLogin = "79961163172";
    public const string InvalidPassword = "wrongpass123";
    public const string UserNotFound = "Пользователь не найден";
    public const string InvalidPasswordError = "Неверный логин или пароль";
    
    public const string SecretPassword = "SECRET_PASSWORD";
}