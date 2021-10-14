using System;
using System.Threading.Tasks;
using FluentValidation.Results;
using NSE.Core.Messages;

namespace NSE.Core.Mediator
{
    public interface IMediatorHandler
    {
        Task<ValidationResult> SendCommand<T>(T command) where T : Command;
        Task PublishEvent<T>(T eventParam) where T : Event;
    }
}
