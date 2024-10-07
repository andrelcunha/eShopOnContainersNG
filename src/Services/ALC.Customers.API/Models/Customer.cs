using System;
using ALC.Core.DomainObjects;

namespace ALC.Customers.API.Models;

public class Customer : Entity, IAggregationRoot
{
    public string Name { get; private set; } = string.Empty;
    public Email Email { get; private set; }
    public Cpf Cpf { get; private set; }
    public bool Excluded { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public Address? Address { get; private set; }

    public Customer() { } //EF 

    public Customer(Guid id,  string name, string email, string cpf)
    {
        Id = id;
        Name = name;
        Email = new Email(email);
        Cpf = new Cpf(cpf);
        Excluded = false;
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;
    }

    public void ChangeEmail(string email)
    {
        Email = new Email(email);
    }

    public void AddAddress(Address address)
    {
        Address = address;
    }
}
