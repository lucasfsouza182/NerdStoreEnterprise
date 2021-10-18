using System.Collections.Generic;
using System.Threading.Tasks;
using NSE.Core.Data;

namespace NSE.Customers.API.Models
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        void Add(Customer client);
        Task<IEnumerable<Customer>> GetAll();
        Task<Customer> GetByCpf(string cpf);
    }
}
