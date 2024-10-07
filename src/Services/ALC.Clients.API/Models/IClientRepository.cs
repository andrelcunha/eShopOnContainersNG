using ALC.Core.Data;

namespace ALC.Clients.API.Models;

public interface IClientRepository : IRepository<Client>
{
    void Add(Client client);
    Task<IEnumerable<Client>> GetAll();
    Task<Client> GetByCpf(string cpf);
}