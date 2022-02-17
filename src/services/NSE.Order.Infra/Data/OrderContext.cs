using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using NSE.Core.Data;
using NSE.Core.Mediator;
using NSE.Core.Messages;
using System.Linq;
using System.Threading.Tasks;

namespace NSE.Order.Infra.Data
{
    public class OrderContext : DbContext, IUnitOfWork
    {
        private readonly IMediatorHandler _mediatorHandler;

        public OrderContext(DbContextOptions<OrderContext> options, IMediatorHandler mediatorHandler) 
            : base(options)
        {
            _mediatorHandler = mediatorHandler;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            modelBuilder.Ignore<Event>();
            modelBuilder.Ignore<ValidationResult>();

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }

        public async Task<bool> Commit()
        {
            var success = await base.SaveChangesAsync() > 0;

            return success;
        }
    }
}
