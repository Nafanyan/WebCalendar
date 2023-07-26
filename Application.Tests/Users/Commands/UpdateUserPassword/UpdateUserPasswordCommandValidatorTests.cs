using Application.Users.Commands.UpdateUserPassword;
using Application.Validation;
using Domain.Entities;
using Domain.Repositories;
using Domain.UnitOfWork;
using Infrastructure.Entities.Users;
using Infrastructure.Foundation;
using Microsoft.EntityFrameworkCore;

namespace Application.Tests.Users.Commands.UpdateUserPassword
{
    public class UpdateUserPasswordCommandValidatorTests
    {
        private IAsyncValidator<UpdateUserPasswordCommand> _validator;

        [SetUp]
        public async Task Setup()
        {
            string dbName = $"EventDb_{DateTime.Now.ToFileTimeUtc()}";
            DbContextOptions<WebCalendarDbContext> dbContextOptions = new DbContextOptionsBuilder<WebCalendarDbContext>()
                .UseInMemoryDatabase(dbName)
                .Options;
            WebCalendarDbContext webCalendarDbContext = new WebCalendarDbContext(dbContextOptions);

            IUserRepository userRepository = new UserRepository(webCalendarDbContext);
            await InitData(userRepository, webCalendarDbContext);

            _validator = new UpdateUserPasswordCommandValidator(userRepository);
        }

        [Test]
        public async Task ValidationAsync_IdNotFound_Fail()
        {
            // arrange
            UpdateUserPasswordCommand updateUserPasswordCommand = new UpdateUserPasswordCommand
            {
                Id = 0
            };

            // act
            ValidationResult result = await _validator.ValidationAsync(updateUserPasswordCommand);

            // assert
            Assert.IsTrue(result.IsFail);
        }

        [Test]
        public async Task ValidationAsync_InputPasswordHashNotEqualOldPasswordHash_Fail()
        {
            // arrange
            UpdateUserPasswordCommand updateUserPasswordCommand = new UpdateUserPasswordCommand
            {
                Id = 1,
                OldPasswordHash = "Invalide Password Hash"
            };

            // act
            ValidationResult result = await _validator.ValidationAsync(updateUserPasswordCommand);

            // assert
            Assert.IsTrue(result.IsFail);
        }

        private async Task InitData(IUserRepository userRepository, WebCalendarDbContext webCalendarDbContext)
        {
            User user = new User("login", "passwordHash");
            userRepository.Add(user);

            IUnitOfWork unitOfWork = new UnitOfWork(webCalendarDbContext);
            await unitOfWork.CommitAsync();
        }
    }
}
