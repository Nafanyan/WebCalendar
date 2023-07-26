using Application.Entities;
using Application.Repositories;
using Application.Users.Commands.UpdateUserLogin;
using Application.Validation;
using Infrastructure.Entities.Users;
using Infrastructure.Foundation;
using Microsoft.EntityFrameworkCore;

namespace Application.Tests.Users.Commands.UpdateUserLogin
{
    public class UpdateUserLoginCommandValidatorTests
    {
        private IAsyncValidator<UpdateUserLoginCommand> _validator;

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

            _validator = new UpdateUserLoginCommandValidator(userRepository);
        }

        [Test]
        public async Task ValidationAsync_LoginIsNull_Fail()
        {
            // arrange
            UpdateUserLoginCommand updateUserLoginCommand = new UpdateUserLoginCommand
            {
                Login = null
            };

            // act
            ValidationResult result = await _validator.ValidationAsync(updateUserLoginCommand);

            // assert
            Assert.IsTrue(result.IsFail);
        }

        [Test]
        public async Task ValidationAsync_LoginIsEmpty_Fail()
        {
            // arrange
            UpdateUserLoginCommand updateUserLoginCommand = new UpdateUserLoginCommand
            {
                Login = "",
            };

            // act
            ValidationResult result = await _validator.ValidationAsync(updateUserLoginCommand);

            // assert
            Assert.IsTrue(result.IsFail);
        }

        [Test]
        public async Task ValidationAsync_IdNotFound_Fail()
        {
            // arrange
            UpdateUserLoginCommand updateUserLoginCommand = new UpdateUserLoginCommand
            {
                Id = 0
            };

            // act
            ValidationResult result = await _validator.ValidationAsync(updateUserLoginCommand);

            // assert
            Assert.IsTrue(result.IsFail);
        }

        [Test]
        public async Task ValidationAsync_LoginNotFound_Fail()
        {
            // arrange
            UpdateUserLoginCommand updateUserLoginCommand = new UpdateUserLoginCommand
            {
                Id = 1,
                Login = "login"
            };

            // act
            ValidationResult result = await _validator.ValidationAsync(updateUserLoginCommand);

            // assert
            Assert.IsTrue(result.IsFail);
        }

        [Test]
        public async Task ValidationAsync_PasswordHashIsInvalid_Fail()
        {
            // arrange
            UpdateUserLoginCommand updateUserLoginCommand = new UpdateUserLoginCommand
            {
                Id = 1,
                Login = "login",
                PasswordHash = "Invalide Password Hash"
            };

            // act
            ValidationResult result = await _validator.ValidationAsync(updateUserLoginCommand);

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
