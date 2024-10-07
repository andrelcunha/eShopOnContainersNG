using ALC.Core.Data;

namespace ALC.Customers.API.Models;

public interface ICustomerRepository : IRepository<Customer>
{
    void Add(Customer Customer);
    Task<IEnumerable<Customer>> GetAll();
    Task<Customer> GetByCpf(string cpf);
}