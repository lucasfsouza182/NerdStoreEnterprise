using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NSE.Cart.API.Model
{
    public class Voucher
    {
        public string Code { get; set; }
        public decimal? Percentage { get; set; }
        public decimal? ValueDiscount { get; set; }
        public TypeDiscountVoucher TypeDiscount { get; set; }
    }

    public enum TypeDiscountVoucher
    {
        Percentage = 0,
        Value = 1
    }
}
