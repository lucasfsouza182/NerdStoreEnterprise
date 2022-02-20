namespace NSE.Bff.Shop.Models
{
    public class VoucherDTO
    {
        public string Code { get; set; }
        public decimal? Percentage { get; set; }
        public decimal? ValueDiscount { get; set; }
        public int TypeDiscount { get; set; }
    }
}
