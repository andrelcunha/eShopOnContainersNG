using ALC.Core.Messages;
using FluentValidation.Results;

namespace ALC.Core.Mediator;

public interface IMediatorHandler
{
    Task PublishEvent<T>(T eventObj) where T : Event;
    Task<ValidationResult> SendCommand<T>(T command) where T : Command;
}
