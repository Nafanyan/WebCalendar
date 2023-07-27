using Application.Entities;
using Application.Repositories;
using Application.Users.Commands.DeleteUser;
using Application.Validation;
using Infrastructure.Entities.Users;
using Infrastructure.Foundation;
using Microsoft.EntityFrameworkCore;

namespace Application.Tests.Users.Commands.DeleteUser
{
    public class DeleteUserCommandValidatorTests
    {
        private IAsyncValidator<DeleteUserCommand> _validator;

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

            _validator = new DeleteUserCommandValidator( userRepository );
        }

        [Test]
        public async Task ValidationAsync_IdNotFound_Fail()
        {
            // arrange
            DeleteUserCommand deleteUserCommand = new DeleteUserCommand
            {
                Id = 0
            };

            // act
            ValidationResult result = await _validator.ValidationAsync( deleteUserCommand );

            // assert
            Assert.IsTrue( result.IsFail );
        }

        [Test]
        public async Task ValidationAsync_PasswordHashIsInvalid_Fail()
        {
            // arrange
            DeleteUserCommand deleteUserCommand = new DeleteUserCommand
            {
                Id = 1,
                PasswordHash = "Invalide Password Hash"
            };

            // act
            ValidationResult result = await _validator.ValidationAsync( deleteUserCommand );

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
