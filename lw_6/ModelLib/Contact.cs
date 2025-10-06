namespace ModelLib;

public class Contact
{
    private String _firstName;
    private String _middleName;
    private String _lastName;

    private readonly List<PhoneNumber> _phoneNumbers;

    private PhoneNumber? _primaryPhoneNumber;

    public Contact(String firstName, String? middleName = "", String? lastName = "")
    {
        if (String.IsNullOrEmpty(firstName.Trim()))
        {
            throw new ArgumentException("Имя - не может быть пустой строкой", nameof(firstName));
        }

        _firstName = firstName.Trim();
        _middleName = middleName?.Trim() ?? "";
        _lastName = lastName?.Trim() ?? "";
        _phoneNumbers = [];
    }

    /// <summary>
    /// Имя - не может быть пустой строкой
    /// </summary>
    public String FirstName
    {
        get { return _firstName; }
        set
        {
            if (String.IsNullOrEmpty(value.Trim()))
            {
                throw new ArgumentException("Имя - не может быть пустой строкой", nameof(value));
            }

            _firstName = value.Trim();
        }
    }

    /// <summary>
    /// Отчество
    /// </summary>
    public String MiddleName
    {
        get { return _middleName; }
        set { _middleName = value?.Trim() ?? ""; }
    }

    /// <summary>
    /// Фамилия
    /// </summary>
    public String LastName
    {
        get { return _lastName; }
        set { _lastName = value?.Trim() ?? ""; }
    }

    /// <summary>
    /// Возвращает все номера телефонов.
    /// Номера не могут быть изменены извне так как PhoneNumber - неизменяемый класс.
    /// Список номеров не может быть изменён извне, так как возвращается IReadOnlyList.
    /// </summary>
    public IReadOnlyList<PhoneNumber> PhoneNumbers => _phoneNumbers;


    /// <summary>
    /// Возвращает основной номер телефона
    /// </summary>
    public PhoneNumber? PrimaryPhoneNumber => _primaryPhoneNumber;


    /// <summary>
    /// Удаляет номер телефона
    /// </summary>
    public void RemovePhoneNumber(PhoneNumber value)
    {
        ArgumentNullException.ThrowIfNull(value);
        // Правило было отключено так как метод по ТЗ ничего не должен возвращать, а предупреждение CA1868 - рекомендует возвращать логическое значение
#pragma warning disable CA1868
        if (_phoneNumbers.Contains(value))
#pragma warning restore CA1868
        {
            _phoneNumbers.Remove(value);
            if (_phoneNumbers.Count == 0)
            {
                _primaryPhoneNumber = null;
            } else if (Equals(_primaryPhoneNumber, value))
            {
                _primaryPhoneNumber = _phoneNumbers[0];
            }
        }
    }

    /// <summary>
    /// Добавляет номер телефона либо ничего не делает, если номер уже был
    /// </summary>
    public void AddPhoneNumber(PhoneNumber value)
    {
        if (_phoneNumbers.Count == 0)
        {
            SetPrimaryPhoneNumber(value);
        }

        ArgumentNullException.ThrowIfNull(value);

        if (_phoneNumbers.Contains(value))
        {
            return;
        }

        _phoneNumbers.Add(value);
    }

    /// <summary>
    /// Меняет основной номер телефона
    /// </summary>
    public void SetPrimaryPhoneNumber(PhoneNumber value)
    {
        ArgumentNullException.ThrowIfNull(value);

        if (!_phoneNumbers.Contains(value))
        {
            _phoneNumbers.Add(value);
        }

        _primaryPhoneNumber = value;
    }
}