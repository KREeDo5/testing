namespace ModelLib.Tests;

public class PhoneNumberTests
{
    /// Тест инициализации номера телефона с некорректным номером
    [Theory]
    [MemberData(nameof(ExceptionInitPhoneNumberTestData))]
    public void Cannot_Init_Phone_Number(String text)
    {
        Assert.Throws<ArgumentException>(() => new PhoneNumber(text));
    }

    public static TheoryData<string> ExceptionInitPhoneNumberTestData()
    {
        return new TheoryData<string>
        {
            // Пустая строка
            { "" },
            // Строка из пробелов
            { " " },
            // Разделитель без номера верхний регистр
            { "X" },
            // Разделитель без номера нижний регистр
            { "x" },
            // Лишние разделители без номера
            { "xxx" },
            // Лишние разделители без дополнительного номера
            { "1xXx" },
            // Лишние разделители без основного номера
            { "xxx1" },
            // Номера нет, лишний символ
            { "%" },
            // Номер есть, но с лишним символом
            { "12=34" },
            // Плюс есть, номер отсутствует
            { "+%" },
            // Разделитель и плюс есть, номер отсутствует
            { "+X" },
            // Перед корректным номером лишний плюс
            { "++7999888X777" },
            // Перед корректным номером лишний символ
            { "$+7999888X777" },
            // Перед разделителем отсутствует основной номер
            { "x0-2" },
            // Перед разделителем отсутствует основной номер, есть лишний знак
            { "=x0-" },
            // После разделителя отсутствует дополнительный номер
            { "0-2x" },
        };
    }

    /// Тест инициализации номера телефона с длинным номером
    [Fact]
    public void Cannot_Init_Out_Of_Range_Phone_Number()
    {
        // Слишком длинный основной номер
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            new PhoneNumber("+1-999-555-9999-0000-333-1x999-555-9999-0000-3333"));
        // Слишком длинный дополнительный номер
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            new PhoneNumber("+1-999-555-9999-0000-333x999-555-9999-0000-3333-1"));
    }

    [Theory]
    [MemberData(nameof(MainNumberTestData))]
    public void Can_get_main_number(String text, String expected)
    {
        PhoneNumber phoneNumber = new PhoneNumber(text);
        String result = phoneNumber.Number;
        Assert.Equal(expected, result);
    }

    public static TheoryData<String, String> MainNumberTestData()
    {
        return new TheoryData<String, String>
        {
            // Основные сценарии
            { "1234455", "+1234455" },
            { "1234455x2", "+1234455" },
            { "+7 999 888 77 66", "+79998887766" },
            { "+7 999  888   77 66", "+79998887766" },
            { "+7 (8362) 45-02-72", "+78362450272" },
            { "7 (8362) 45-02-72", "+78362450272" },
            { "8 (8362) 45-02-72", "+88362450272" },
            // Граничные значения
            { "1", "+1" },
            { "+1-999-555-9999-0000-333", "+199955599990000333" },
        };
    }

    [Theory]
    [MemberData(nameof(AdditionalNumberTestData))]
    public void Can_get_additional_number(String text, String expected)
    {
        PhoneNumber phoneNumber = new PhoneNumber(text);
        String result = phoneNumber.Ext;
        Assert.Equal(expected, result);
    }

    public static TheoryData<String, String> AdditionalNumberTestData()
    {
        return new TheoryData<String, String>
        {
            { "1234455", "" },
            //ENG - x
            { "1234455x2", "2" },
            //ENG - X
            { "1234455X2", "2" },
            //RUS - х
            { "1234455x2", "2" },
            //RUS - Х
            { "1234455X2", "2" },
        };
    }


    [Theory]
    [MemberData(nameof(SerializePhoneToStringTestData))]
    public void Can_Serialize_To_String(String text, String expected)
    {
        PhoneNumber phoneNumber = new PhoneNumber(text);
        String result = phoneNumber.ToString();
        Assert.Equal(expected, result);
    }

    public static TheoryData<String, String> SerializePhoneToStringTestData()
    {
        return new TheoryData<String, String>
        {
            { "1234455", "+1234455" },
            { "1234455x2", "+1234455x2" },
            { "+78362450272", "+78362450272" },
            { "+12345678901x1234", "+12345678901x1234" },
            { "+1-999-555-9999-0000-333x999-555-9999-0000-333", "+199955599990000333x99955599990000333" },
        };
    }
    
    /// Тест сравнения объектов PhoneNumber
    [Fact]
    public void Can_Find_The_Same_Phone_Number()
    {
        PhoneNumber a = new PhoneNumber("1234455x2");
        PhoneNumber b = new PhoneNumber("+12-34(4)5-5x2");
        Assert.Equal(a, b);
    }
}