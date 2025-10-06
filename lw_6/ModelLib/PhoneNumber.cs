using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace ModelLib;

/// <summary>
/// Представляет телефонный номер в международном формате.
/// </summary>
public class PhoneNumber
{
    // Разделитель между основным номером и добавочным
    private const Char Separator = 'x';
    
    // Приватное поле для хранения основного номера телефона
    private readonly long _number;

    // Приватное поле для хранения добавочного номера телефона
    private readonly long? _ext;
    
    public PhoneNumber(String text)
    {
        ArgumentNullException.ThrowIfNull(text);
        text = NormalizeText(text);
        if (text.Length == 0)
        {
            throw new ArgumentException("Номер телефона не может быть пустым", nameof(text));
        }

        if (!IsValidFormat(text))
        {
            throw new ArgumentException("Недопустимый формат. Разрешены только цифры и один разделитель между ними.",
                nameof(text));
        }

        // Разделяет строку на основной номер и дополнительный по разделителю.
        String[] parts = text.Split(Separator, 2);
        if (parts[0] == "")
        {
            throw new ArgumentException("Номер телефона не может быть пустым", nameof(text));
        }

        // Проверка длины основного номера. Максимум 18 цифр (номера в Германии).
        if (parts[0].Length > 18)
        {
            throw new ArgumentOutOfRangeException(nameof(text), "Недопустимая длина номера телефона");
        }

        _number = long.Parse(parts[0]);

        if (parts.Length > 1 && parts[1] != "")
        {
            // Проверка длины дополнительного номера.
            if (parts[1].Length > 18)
            {
                throw new ArgumentOutOfRangeException(nameof(text), "Недопустимая длина добавочного номера телефона");
            }

            _ext = long.Parse(parts[1]);
        }
    }
    
    /// <summary>
    /// Возвращает строку номера телефона с символом + в начале, но без добавочного номера
    /// </summary>
    public String Number => "+" + _number;

    /// <summary>
    /// Возвращает добавочный номер либо пустую строку
    /// </summary>
    public String Ext => _ext?.ToString() ?? "";

    /// <summary>
    /// Возвращает строку номера телефона с символом + в начале
    /// </summary>
    public override String ToString() => _ext == null ? $"+{_number}" : $"+{_number}{Separator}{_ext}";

    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        if (obj is PhoneNumber other)
        {
            return Equals(other);
        }

        return false;
    }
    
    // Проверяет оставшиеся символы на соответствие формату. Разрешены только цифры и один разделитель между ними.
    private static bool IsValidFormat(String text)
    {
        String pattern = $@"^(\d+([{Separator}]\d+)?|\d*)$";
        return Regex.IsMatch(text, pattern, RegexOptions.IgnoreCase);
    }

    // Убирает пробелы, "+" в начале, заменяет X на x
    private static String NormalizeText(String text)
    {
        if (text.Length == 0)
        {
            return text;
        }

        // Удаляет пробелы, дефисы, круглые скобки
        text = Regex.Replace(text, @"[\s\-\(\)]", "");

        if (text.StartsWith("+"))
        {
            text = text[1..];
        }

        // Заменяет все X/x/Х/х на Separator
        text = Regex.Replace(text, "[XxХх]", Separator.ToString());

        return text;
    }

    /// <summary>
    ///  Проверяет равенство двух номеров.
    /// </summary>
    private bool Equals(PhoneNumber other)
    {
        return other._number == _number && other._ext == _ext;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_number, _ext);
    }
}