using System;
using System.Text.RegularExpressions;

namespace ALC.Core.DomainObjects;

public class Email
{
    public const int AddressMaxLength = 254;
    public const int AddressMinLength = 5;
    public string Address { get; private set; } = string.Empty;
    

    public Email(string address)
    {
        if (!IsValid(address)) throw new DomainException("Invalid email.");
        Address = address;
    }

    public static bool IsValid(string email)
    {
        var regexEmail = new Regex(@"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");
        return regexEmail.IsMatch(email);
    }
}
