using Application.Users.Commands.CreateUser;
using Application.Validation;
using Domain.Entities;
using Domain.Repositories;
using Domain.UnitOfWork;
using Infrastructure.Entities.Users;
using Infrastructure.Foundation;
using Microsoft.EntityFrameworkCore;

namespace Application.Tests.Users.Commands.CreateUser
{
    public class CreateUserCommandValidatorTests
    {
        private IAsyncValidator<CreateUserCommand> _validator;

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

            _validator = new CreateUserCommandValidator(userRepository);
        }

        [Test]
        public async Task ValidationAsync_LoginIsNull_Fail()
        {
            // arrange
            CreateUserCommand createUserCommand = new CreateUserCommand
            {
                Login = null
            };

            // act
            ValidationResult result = await _validator.ValidationAsync(createUserCommand);

            // assert
            Assert.IsTrue(result.IsFail);
        }

        [Test]
        public async Task ValidationAsync_LoginIsEmpty_Fail()
        {
            // arrange
            CreateUserCommand createUserCommand = new CreateUserCommand
            {
                Login = ""
            };

            // act
            ValidationResult result = await _validator.ValidationAsync(createUserCommand);

            // assert
            Assert.IsTrue(result.IsFail);
        }

        [Test]
        public async Task ValidationAsync_LoginOver28Chars_Fail()
        {
            // arrange
            CreateUserCommand createUserCommand = new CreateUserCommand
            {
                Login = "123456789 123456789 123456789"
            };

            // act
            ValidationResult result = await _validator.ValidationAsync(createUserCommand);

            // assert
            Assert.IsTrue(result.IsFail);
        }

        [Test]
        public async Task ValidationAsync_LoginAlreadyExist_Fail()
        {
            // arrange
            CreateUserCommand createUserCommand = new CreateUserCommand
            {
                Login = "login"
            };

            // act
            ValidationResult result = await _validator.ValidationAsync(createUserCommand);

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
