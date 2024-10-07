using ALC.Core.DomainObjects;

namespace ALC.Clients.API.Models;

public class Address : Entity
{
    public string Street { get; private set; } = string.Empty;

    public string Number { get; private set; } = string.Empty;
    public string? AdditionalInfo { get; private set; }
    public string Neighborhood { get; private set; } = string.Empty;
    public string PostalCode { get; private set; } = string.Empty;
    public string City { get; private set; } = string.Empty;
    public string State { get; private set; } = string.Empty;
    public string Country { get; private set; } = string.Empty;
    public Guid ClientId { get; private set; }

    public Client? Client { get; private set; }

    public Address() {} //EF

    public Address(string street, string number, string? additionalInfo, string neighborhood, string postalCode, string city, string state, string country)
    {
        Street = street;
        Number = number;
        AdditionalInfo = additionalInfo;
        Neighborhood = neighborhood;
        PostalCode = postalCode;
        City = city;
        State = state;
        Country = country;
    }    
}