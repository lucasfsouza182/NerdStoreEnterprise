using FluentValidation;
using System;
using System.Text.Json.Serialization;

namespace NSE.Cart.API.Model
{
    public class CartItem
    {
        internal const int MAX_QUANTIDADE_ITEM = 5;

        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public Guid CartId { get; set; }

        [JsonIgnore]
        public CartCustomer CartCustomer { get; set; }

        public CartItem()
        {
            Id = Guid.NewGuid();
        }

        internal void SetCart(Guid cartId)
        {
            CartId = cartId;
        }

        internal decimal CalculateValue()
        {
            return Amount * Price;
        }

        internal void AddUnits(int units)
        {
            Amount += units;
        }

        internal void UpdateUnits(int units)
        {
            Amount = units;
        }

        internal bool IsValid()
        {
            return new CartItemValidation().Validate(this).IsValid;
        }

        public class CartItemValidation : AbstractValidator<CartItem>
        {
            public CartItemValidation()
            {
                RuleFor(c => c.ProductId)
                    .NotEqual(Guid.Empty)
                    .WithMessage("Product id invalid");

                RuleFor(c => c.Name)
                    .NotEmpty()
                    .WithMessage("Product name not provided");

                RuleFor(c => c.Amount)
                    .GreaterThan(0)
                    .WithMessage(item => $"Minimum quantity for {item.Name} is 1");

                RuleFor(c => c.Amount)
                    .LessThanOrEqualTo(CartCustomer.MAX_QUANTIDADE_ITEM)
                    .WithMessage(item => $"The maximum quantity of {item.Name} is {CartCustomer.MAX_QUANTIDADE_ITEM}");

                RuleFor(c => c.Price)
                    .GreaterThan(0)
                    .WithMessage(item => $"The value of {item.Name} must be greater than 0");
            }
        }
    }
}
