﻿using Application.Result;

namespace Application.Interfaces
{
    public interface ICommandHandler<TCommand> where TCommand : class
    {
        Task<CommandResult> HandleAsync( TCommand command );
    }

    public interface ICommandHandler<TResult, TCommand>
    where TResult : class
    where TCommand : class
    {
        Task<CommandResult<TResult>> HandleAsync( TCommand command );
    }
}
