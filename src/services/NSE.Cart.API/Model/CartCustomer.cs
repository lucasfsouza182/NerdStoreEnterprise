using System;
using System.Collections.Generic;
using System.Linq;

namespace NSE.Cart.API.Model
{
    public class CartCustomer
    {
        internal const int MAX_QUANTIDADE_ITEM = 5;

        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public decimal TotalValue { get; set; }
        public List<CartItem> Itens { get; set; } = new List<CartItem>();

        public CartCustomer(Guid customerId)
        {
            Id = Guid.NewGuid();
            CustomerId = customerId;
        }

        // EF
        public CartCustomer() { }


        internal void CalculateValueCart()
        {
            TotalValue = Itens.Sum(p => p.CalculateValue());
        }

        internal bool CartExistedItem(CartItem item)
        {
            return Itens.Any(p => p.ProductId == item.ProductId);
        }

        internal CartItem GetByProductId(Guid productId)
        {
            return Itens.FirstOrDefault(p => p.ProductId == productId);
        }

        internal void AddItem(CartItem item)
        {
            item.SetCart(Id);

            if (CartExistedItem(item))
            {
                var existedItem = GetByProductId(item.ProductId);
                existedItem.AddUnits(item.Amount);

                item = existedItem;
                Itens.Remove(existedItem);
            }

            Itens.Add(item);
            CalculateValueCart();
        }
    }
}
