﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NSE.Customers.API.Models;
using NSE.Core.Data;

namespace NSE.Customers.API.Data.Repository
{
    public class ClientRepository : IClientRepository
    {
        private readonly ClientsContext _context;

        public ClientRepository(ClientsContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<IEnumerable<Client>> GetAll()
        {
            return await _context.Clients.AsNoTracking().ToListAsync();
        }

        public Task<Client> GetByCpf(string cpf)
        {
            return _context.Clients.FirstOrDefaultAsync(c => c.Cpf.Number == cpf);
        }

        public void Add(Client client)
        {
            _context.Clients.Add(client);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
