using System.Threading.Tasks;
using FluentValidation.Results;
using NSE.Core.Data;

namespace NSE.Core.Messages
{
    public abstract class CommandHandler
    {
        protected ValidationResult ValidationResult;

        protected CommandHandler()
        {
            ValidationResult = new ValidationResult();
        }

        protected void AddError(string message)
        {
            ValidationResult.Errors.Add(new ValidationFailure(string.Empty, message));
        }

        protected async Task<ValidationResult> SaveData(IUnitOfWork uow)
        {
            if (!await uow.Commit())
            {
                AddError("There was an error persisting the data.");
            }

            return ValidationResult;
        }
    }
}
