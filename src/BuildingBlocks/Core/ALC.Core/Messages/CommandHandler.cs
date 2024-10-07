using ALC.Core.Data;
using FluentValidation.Results;

namespace ALC.Core.Messages;

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

    protected async Task<ValidationResult> PersistData(IUnitOfWork unitOfWork)
    {
        if (!await unitOfWork.Commit()) 
        AddError("An error occurred while persisting data");

        return ValidationResult;
    }
}
