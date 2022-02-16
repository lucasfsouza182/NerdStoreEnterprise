﻿using System.Collections.Generic;

namespace NSE.Bff.Shop.Models
{
    public class CartDTO
    {
        public decimal TotalValue { get; set; }
        public decimal Discount { get; set; }
        public List<CartItemDTO> Itens { get; set; } = new List<CartItemDTO>();
    }
}
