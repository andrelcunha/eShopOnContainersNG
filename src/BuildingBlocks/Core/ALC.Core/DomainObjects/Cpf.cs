using System;
using System.Runtime.CompilerServices;
using ALC.Core.Utils;

namespace ALC.Core.DomainObjects;

public class Cpf
{
    public const int CpfMaxLength = 11;
    public string Number { get; private set; } = string.Empty;

    public Cpf(string number)
    {
        if (!IsValid(number)) throw new DomainException("Invalid CPF.");
        Number = number;
    }

    public static bool IsValid(string cpf)
    {
        cpf = cpf.OnlyNumbers();

        if (cpf.Length > CpfMaxLength) 
            return false;

        while(cpf.Length != CpfMaxLength)
            cpf = '0' + cpf;
        
        var equal = true;

        for (var i = 1; i < CpfMaxLength && equal; i++)
        {
            if(cpf[i] != cpf[0])
            equal = false;
        }

        if (equal || cpf == "12345678909")
            return false;

        var numbers = new int[CpfMaxLength];

        for (var i = 0; i < CpfMaxLength; i++)
            numbers[i] = int.Parse(cpf[i].ToString());
        
        var sum = 0;
        for (var i = 0; i < 9; i++)
            sum += (10 - i) * numbers[i];

        var result = sum % 11;

        if (result == 1 || result == 0)
        {
            if (numbers[10] != 0)
                return false;
        }
        else if (numbers[10] != 11 - result)
            return false;
        
        return true;
    }
}
