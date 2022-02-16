using System;

namespace NSE.Bff.Shop.Models
{
    public class ProductItemDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public int QuantityStock { get; set; }
    }
}
