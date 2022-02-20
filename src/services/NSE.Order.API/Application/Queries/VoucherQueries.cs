using NSE.Order.API.Application.DTO;
using NSE.Order.Domain.Vouchers;
using System.Threading.Tasks;

namespace NSE.Order.API.Application.Queries
{
    public interface IVoucherQueries
    {
        Task<VoucherDTO> GetVoucherByCode(string code);
    }

    public class VoucherQueries : IVoucherQueries
    {
        private readonly IVoucherRepository _voucherRepository;

        public VoucherQueries(IVoucherRepository voucherRepository)
        {
            _voucherRepository = voucherRepository;
        }

        public async Task<VoucherDTO> GetVoucherByCode(string code)
        {
            var voucher = await _voucherRepository.GetVoucherByCode(code);

            if (voucher == null) return null;

            if (!voucher.IsValidToBeUsed()) return null;

            return new VoucherDTO
            {
                Code = voucher.Code,
                TypeDiscount = (int)voucher.TypeDiscount,
                Percentage = voucher.Percentage,
                ValueDiscount = voucher.ValueDiscount
            };
        }
    }
}
