using System;
using ALC.Clients.API.Models;
using ALC.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace ALC.Clients.API.Data.Repository;

public class ClientRepository : IClientRepository
{
    private readonly ClientContext _context;

    public ClientRepository(ClientContext context)
    {
        _context = context;
    }

    public IUnitOfWork UnitOfWork => _context;

    public void Add(Client client)
    {
        _context.Clients.Add(client);
    }

    public async Task<IEnumerable<Client>> GetAll()
    {
        return await _context.Clients.AsNoTracking().ToListAsync();
    }

    public async Task<Client> GetByCpf(string cpf)
    {
        return await _context.Clients.FirstOrDefaultAsync(c => c.Cpf.Number == cpf);
    }

    public void Dispose()
    {
        _context?.Dispose();
    }
}
