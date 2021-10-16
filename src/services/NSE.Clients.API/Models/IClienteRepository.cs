using System.Collections.Generic;
using System.Threading.Tasks;
using NSE.Core.Data;

namespace NSE.Clients.API.Models
{
    public interface IClientRepository : IRepository<Client>
    {
        void Add(Client client);
        Task<IEnumerable<Client>> GetAll();
        Task<Client> GetByCpf(string cpf);
    }
}
