using System;
using NSE.Core.DomainObjects;

namespace NSE.Catalog.API.Models
{
    public class Product : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Image { get; set; }
        public int QuantityStock { get; set; }
    }
}
