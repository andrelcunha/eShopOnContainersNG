using System;

namespace ALC.Core.Messages.Integration;

public class UserRegisteredIntegrationEvent
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Cpf { get; set; }
    
    public UserRegisteredIntegrationEvent(Guid id, string name, string email, string cpf)
    {
        Id = id;
        Name = name;
        Email = email;
        Cpf = cpf;
    }
}
