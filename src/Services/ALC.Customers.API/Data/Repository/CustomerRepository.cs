using System;
using ALC.Customers.API.Models;
using ALC.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace ALC.Customers.API.Data.Repository;

public class CustomerRepository : ICustomerRepository
{
    private readonly CustomerContext _context;

    public CustomerRepository(CustomerContext context)
    {
        _context = context;
    }

    public IUnitOfWork UnitOfWork => _context;

    public void Add(Customer Customer)
    {
        _context.Customers.Add(Customer);
    }

    public async Task<IEnumerable<Customer>> GetAll()
    {
        return await _context.Customers.AsNoTracking().ToListAsync();
    }

    public async Task<Customer> GetByCpf(string cpf)
    {
        return await _context.Customers.FirstOrDefaultAsync(c => c.Cpf.Number == cpf);
    }

    public void Dispose()
    {
        _context?.Dispose();
    }
}
