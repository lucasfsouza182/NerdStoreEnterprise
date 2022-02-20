namespace NSE.Order.API.Application.DTO
{
    public class VoucherDTO
    {
        public string Code { get; set; }
        public decimal? Percentage { get; set; }
        public decimal? ValueDiscount { get; set; }
        public int TypeDiscount { get; set; }
    }


}
