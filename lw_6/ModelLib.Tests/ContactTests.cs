namespace ModelLib.Tests;

public class ContactTests
{
    /// Негативные тесты инициализации контакта телефона с пустым именем
    [Fact]
    public void Cannot_Init_Contact()
    {
        Assert.Throws<ArgumentException>(() => new Contact(""));
        Assert.Throws<ArgumentException>(() => new Contact("    "));
    }

    /// Тесты инициализации контакта
    [Theory]
    [MemberData(nameof(CorrectInitContactTestData))]
    public void Correct_Init_Contact(Dictionary<string, string?> input, Dictionary<string, string> expected)
    {
        Contact value = new Contact(input["FirstName"]!, input["MiddleName"], input["LastName"]);
        Assert.Equal(expected["FirstName"], value.FirstName);
        Assert.Equal(expected["MiddleName"], value.MiddleName);
        Assert.Equal(expected["LastName"], value.LastName);
        Assert.Empty(value.PhoneNumbers);
        Assert.Null(value.PrimaryPhoneNumber);
    }

    public static TheoryData<Dictionary<string, string?>, Dictionary<string, string>> CorrectInitContactTestData()
    {
        return new TheoryData<Dictionary<string, string?>, Dictionary<string, string>>
        {
            // Стандартный сценарий
            {
                new Dictionary<string, string?>
                {
                    { "FirstName", "Тюлень" }, { "MiddleName", "Тестович" }, { "LastName", "Тестовый" }
                },
                new Dictionary<string, string>
                {
                    { "FirstName", "Тюлень" }, { "MiddleName", "Тестович" }, { "LastName", "Тестовый" }
                }
            },
            // Передали параметры с лишними пробелами
            {
                new Dictionary<string, string?>
                {
                    { "FirstName", "Тюлень  " }, { "MiddleName", "  Тестович" }, { "LastName", "Тестовый" }
                },
                new Dictionary<string, string>
                {
                    { "FirstName", "Тюлень" }, { "MiddleName", "Тестович" }, { "LastName", "Тестовый" }
                }
            },
            // Передано только Имя
            {
                new Dictionary<string, string?>
                {
                    { "FirstName", "Иван" }, { "MiddleName", null }, { "LastName", null }
                },
                new Dictionary<string, string> { { "FirstName", "Иван" }, { "MiddleName", "" }, { "LastName", "" } }
            },
            // Пустое отчество и фамилия
            {
                new Dictionary<string, string?> { { "FirstName", "Иван" }, { "MiddleName", "" }, { "LastName", "" } },
                new Dictionary<string, string> { { "FirstName", "Иван" }, { "MiddleName", "" }, { "LastName", "" } }
            },
        };
    }


    /// Тест установки имени контакта
    [Fact]
    public void Can_Set_Contact_FirstName()
    {
        Contact value = new Contact("Тест");
        value.FirstName = " Тюлень ";
        Assert.Equal("Тюлень", value.FirstName);
    }

    /// Тест установки пустого имени контакта
    [Fact]
    public void Cannot_Set_Contact_Empty_FirstName()
    {
        Contact value = new Contact("Тест");
        value.FirstName = "Тюлень";
        Assert.Throws<ArgumentException>(() => value.FirstName = "");
        Assert.Throws<ArgumentException>(() => value.FirstName = "   ");
    }

    /// Тест установки отчества контакта
    [Fact]
    public void Can_Set_Contact_MiddleName()
    {
        Contact value = new Contact("Тест", "Тестович", "Тестовый");
        value.MiddleName = "Тюлень";
        Assert.Equal("Тюлень", value.MiddleName);
        value.MiddleName = "   ";
        Assert.Equal("", value.MiddleName);
        value.MiddleName = "окак";
        Assert.Equal("окак", value.MiddleName);
    }

    /// Тест установки фамилии контакта
    [Fact]
    public void Can_Set_Contact_LastName()
    {
        Contact value = new Contact("Тест", "Тестович", "Тестовый");
        value.LastName = "Тюлень";
        Assert.Equal("Тюлень", value.LastName);
        value.LastName = "   ";
        Assert.Equal("", value.LastName);
        value.LastName = "окак";
        Assert.Equal("окак", value.LastName);
    }

    /// Тест установки первого номера телефона
    [Fact]
    public void Can_Set_First_Phone()
    {
        Contact contact = new Contact("Тест", "Тестович", "Тестовый");
        PhoneNumber phone = new PhoneNumber("+7 (999) 888-77-66");
        contact.AddPhoneNumber(phone);
        Assert.Single(contact.PhoneNumbers);
        Assert.Equal(phone, contact.PrimaryPhoneNumber);
        Assert.Contains(phone, contact.PhoneNumbers);
    }

    /// Тест добавления номера - копии (дупликата)
    [Fact]
    public void Add_Phone_Number_Dublicate()
    {
        Contact contact = new Contact("Тест");
        PhoneNumber a = new PhoneNumber("+7 (999) 888-77-66");
        PhoneNumber b = new PhoneNumber("+7 (999) 888-77-66");
        PhoneNumber c = new PhoneNumber("+7 (999) 888-77-66");

        contact.AddPhoneNumber(a);
        contact.AddPhoneNumber(b);
        contact.AddPhoneNumber(c);

        Assert.Single(contact.PhoneNumbers);
        Assert.Equal(a, contact.PrimaryPhoneNumber);
        Assert.Contains(a, contact.PhoneNumbers);
    }

    /// Тест добавления нескольких номеров телефона 
    [Fact]
    public void Can_Add_Multiple_Phone_Numbers()
    {
        Contact contact = new Contact("Тест");
        PhoneNumber a = new PhoneNumber("+7 (999) 888-77-66");
        PhoneNumber b = new PhoneNumber("+7 (999) 888-77-66 x 4444");
        PhoneNumber c = new PhoneNumber("8123456");

        contact.AddPhoneNumber(a);
        contact.AddPhoneNumber(b);
        contact.AddPhoneNumber(c);

        Assert.Equal(3, contact.PhoneNumbers.Count);
        Assert.Equal(a, contact.PrimaryPhoneNumber);
        Assert.Contains(a, contact.PhoneNumbers);
        Assert.Contains(b, contact.PhoneNumbers);
        Assert.Contains(c, contact.PhoneNumbers);
    }

    /// Тест установки основного номера телефона, который уже есть в списке
    [Fact]
    public void Can_Set_Contains_Primary_Phone_Number()
    {
        Contact contact = new Contact("Тест");
        PhoneNumber a = new PhoneNumber("+7 (999) 888-77-66");
        PhoneNumber b = new PhoneNumber("+7 (999) 888-77-66 x 4444");
        PhoneNumber c = new PhoneNumber("8123456");

        contact.AddPhoneNumber(a);
        contact.AddPhoneNumber(b);
        contact.AddPhoneNumber(c);
        contact.SetPrimaryPhoneNumber(c);
        Assert.Equal(c, contact.PrimaryPhoneNumber);
    }

    /// Тест установки основного номера телефона, которого нет в списке
    [Fact]
    public void Can_Set_New_Primary_Phone_Number()
    {
        Contact contact = new Contact("Тест");
        PhoneNumber a = new PhoneNumber("+7 (999) 888-77-66");
        PhoneNumber b = new PhoneNumber("+7 (999) 888-77-66 x 4444");
        PhoneNumber c = new PhoneNumber("8123456");

        contact.AddPhoneNumber(a);
        contact.AddPhoneNumber(b);
        contact.SetPrimaryPhoneNumber(c);
        Assert.Equal(c, contact.PrimaryPhoneNumber);
        Assert.Equal(3, contact.PhoneNumbers.Count);
    }
    
    /// Тест удаления несуществующего номера телефона
    [Fact]
    public void Can_Remove_Non_Existing_Phone_Number()
    {
        Contact contact = new Contact("Тест");
        PhoneNumber a = new PhoneNumber("+7 (999) 888-77-66");
        PhoneNumber c = new PhoneNumber("8123456");

        contact.AddPhoneNumber(a);
        contact.RemovePhoneNumber(c);
        Assert.Equal(a, contact.PrimaryPhoneNumber);
        Assert.Single(contact.PhoneNumbers);
    }
    
    /// Тест удаления существующего номера телефона
    [Fact]
    public void Can_Remove_Existing_Phone_Number()
    {
        Contact contact = new Contact("Тест");
        PhoneNumber c = new PhoneNumber("8123456");
        
        contact.AddPhoneNumber(c);
        contact.RemovePhoneNumber(c);
        Assert.Null(contact.PrimaryPhoneNumber);
        Assert.Empty(contact.PhoneNumbers);
    }
    
    /// Тест удаления основного номера телефона, если есть другие номера
    [Fact]
    public void Can_Set_New_Primary_Phone_Number_Automatically()
    {
        Contact contact = new Contact("Тест");
        PhoneNumber a = new PhoneNumber("+7 (999) 888-77-66");
        PhoneNumber b = new PhoneNumber("8123456");
        PhoneNumber c = new PhoneNumber("1(111)111-111-11-11");
        contact.AddPhoneNumber(a);
        contact.AddPhoneNumber(b);
        contact.AddPhoneNumber(c);
        
        contact.RemovePhoneNumber(contact.PrimaryPhoneNumber!);
        
        Assert.Equal(2, contact.PhoneNumbers.Count);
        Assert.NotNull(contact.PrimaryPhoneNumber);
        Assert.NotEqual(a, contact.PrimaryPhoneNumber!);
    }
}