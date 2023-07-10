using Application.Interfaces;
using Application.Result;
using Application.Validation;
using Domain.Repositories;
using Domain.UnitOfWork;

namespace Application.JWTs.Commands.CreateJWT
{
    public class CreateJWTCommandHandler : ICommandHandler<CreateJWTCommand>
    {
        private readonly IJWTRepository _jWTRepository;
        private readonly IAsyncValidator<CreateJWTCommand> _createJWTCommandValidator;
        private readonly IUnitOfWork _unitOfWork;

        public CreateJWTCommandHandler(
            IJWTRepository jWTRepository,
            IAsyncValidator<CreateJWTCommand> validator,
            IUnitOfWork unitOfWork)
        {
            _jWTRepository = jWTRepository;
            _createJWTCommandValidator = validator;
            _unitOfWork = unitOfWork;
        }

        async Task<CommandResult> ICommandHandler<CreateJWTCommand>.HandleAsync(CreateJWTCommand createJWTCommand)
        {
            ValidationResult validationResult = await _createJWTCommandValidator.ValidationAsync(createJWTCommand);
            if(!validationResult.IsFail)
            {
                //await _jWTRepository.AddAsync();
                await _unitOfWork.CommitAsync();
            }
            return new CommandResult(validationResult);
        }
    }
}
