﻿using System;
using System.Collections.Generic;

namespace NSE.WebAPP.MVC.Models
{
    public class CartViewModel
    {
        public decimal TotalValue { get; set; }
        public List<ProductItemViewModel> Itens { get; set; } = new List<ProductItemViewModel>();
    }

    public class ProductItemViewModel
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
    }
}