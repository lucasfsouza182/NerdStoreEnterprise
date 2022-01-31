using FluentValidation;
using FluentValidation.Results;
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

        public ValidationResult ValidationResult { get; set; }

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

        internal void UpdateItem(CartItem item)
        {
            item.SetCart(Id);

            var existedItem = GetByProductId(item.ProductId);

            Itens.Remove(existedItem);
            Itens.Add(item);

            CalculateValueCart();
        }

        internal void UpdateUnits(CartItem item, int amount)
        {
            item.UpdateUnits(amount);
            UpdateItem(item);
        }

        internal void RemoveItem(CartItem item)
        {
            Itens.Remove(GetByProductId(item.ProductId));
            CalculateValueCart();
        }

        internal bool IsValid()
        {
            var erros = Itens.SelectMany(i => new CartItem.CartItemValidation().Validate(i).Errors).ToList();
            erros.AddRange(new CartCustomerValidation().Validate(this).Errors);
            ValidationResult = new ValidationResult(erros);

            return ValidationResult.IsValid;
        }

        public class CartCustomerValidation : AbstractValidator<CartCustomer>
        {
            public CartCustomerValidation()
            {
                RuleFor(c => c.CustomerId)
                    .NotEqual(Guid.Empty)
                    .WithMessage("Customer not found");

                RuleFor(c => c.Itens.Count)
                    .GreaterThan(0)
                    .WithMessage("The cart does not have items");

                RuleFor(c => c.TotalValue)
                    .GreaterThan(0)
                    .WithMessage("The total cart value must be greater than 0");
            }
        }
    }
}
