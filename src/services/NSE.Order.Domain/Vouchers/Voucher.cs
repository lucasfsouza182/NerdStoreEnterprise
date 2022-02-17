using NSE.Core.DomainObjects;
using System;

namespace NSE.Order.Domain.Vouchers
{
    public class Voucher : Entity, IAggregateRoot
    {
        public string Code { get; private set; }
        public decimal? Percentage { get; private set; }
        public decimal? ValueDiscount { get; private set; }
        public int Quantity { get; private set; }
        public TypeDiscountVoucher TypeDiscount { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public DateTime? UsagedDate { get; private set; }
        public DateTime ExpirationDate { get; private set; }
        public bool Active { get; private set; }
        public bool Used { get; private set; }
    }
}
