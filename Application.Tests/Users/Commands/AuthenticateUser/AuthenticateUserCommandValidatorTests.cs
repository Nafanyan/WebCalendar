using Application.Entities;
using Application.Repositories;
using Application.UserAuthorizationTokens.Commands.AuthenticateUser;
using Application.Validation;
using Infrastructure.Entities.Users;
using Infrastructure.Foundation;
using Microsoft.EntityFrameworkCore;

namespace Application.Tests.Users.Commands.AuthenticateUser
{
    public class AuthenticateUserCommandValidatorTests
    {
        private IAsyncValidator<AuthenticateUserCommand> _validator;

        [SetUp]
        public async Task Setup()
        {
            string dbName = $"EventDb_{DateTime.Now.ToFileTimeUtc()}";
            DbContextOptions<WebCalendarDbContext> dbContextOptions = new DbContextOptionsBuilder<WebCalendarDbContext>()
                .UseInMemoryDatabase( dbName )
                .Options;
            WebCalendarDbContext webCalendarDbContext = new WebCalendarDbContext( dbContextOptions );

            IUserRepository userRepository = new UserRepository( webCalendarDbContext );
            await InitData( userRepository, webCalendarDbContext );

            _validator = new AuthenticateUserCommandValidator( userRepository );
        }

        [Test]
        public async Task ValidationAsync_LoginIsNull_Fail()
        {
            // arrange
            AuthenticateUserCommand authenticateUserCommand = new AuthenticateUserCommand
            {
                Login = null
            };

            // act
            ValidationResult result = await _validator.ValidationAsync( authenticateUserCommand );

            // assert
            Assert.IsTrue( result.IsFail );
        }

        [Test]
        public async Task ValidationAsync_LoginIsEmpty_Fail()
        {
            // arrange
            AuthenticateUserCommand authenticateUserCommand = new AuthenticateUserCommand
            {
                Login = ""
            };

            // act
            ValidationResult result = await _validator.ValidationAsync( authenticateUserCommand );

            // assert
            Assert.IsTrue( result.IsFail );
        }

        [Test]
        public async Task ValidationAsync_LoginIsInvalid_Fail()
        {
            // arrange
            AuthenticateUserCommand authenticateUserCommand = new AuthenticateUserCommand
            {
                Login = "Invalide Login"
            };

            // act
            ValidationResult result = await _validator.ValidationAsync( authenticateUserCommand );

            // assert
            Assert.IsTrue( result.IsFail );
        }

        [Test]
        public async Task ValidationAsync_PasswordHashIsInvalid_Fail()
        {
            // arrange
            AuthenticateUserCommand authenticateUserCommand = new AuthenticateUserCommand
            {
                Login = "login",
                PasswordHash = "Invalide Password Hash"
            };

            // act
            ValidationResult result = await _validator.ValidationAsync( authenticateUserCommand );

            // assert
            Assert.IsTrue( result.IsFail );
        }

        private async Task InitData( IUserRepository userRepository, WebCalendarDbContext webCalendarDbContext )
        {
            User user = new User( "login", "passwordHash" );
            userRepository.Add( user );

            IUnitOfWork unitOfWork = new UnitOfWork( webCalendarDbContext );
            await unitOfWork.CommitAsync();
        }
    }
}
