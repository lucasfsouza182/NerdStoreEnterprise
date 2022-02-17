using Microsoft.EntityFrameworkCore;
using NSE.Core.Data;
using NSE.Order.Domain.Vouchers;
using System.Threading.Tasks;

namespace NSE.Order.Infra.Data.Repository
{
    public class VoucherRepository : IVoucherRepository
    {
        private readonly OrderContext _context;

        public VoucherRepository(OrderContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<Voucher> GetVoucherByCode(string code)
        {
            return await _context.Vouchers.FirstOrDefaultAsync(p => p.Code == code);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
