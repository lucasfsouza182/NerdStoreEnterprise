using System;

namespace NSE.Bff.Shop.Models
{
    public class CartItemDTO
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public int Amount { get; set; }
    }
}
