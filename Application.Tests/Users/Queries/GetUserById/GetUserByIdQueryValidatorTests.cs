using Application.Entities;
using Application.Repositories;
using Application.Users.Queries.GetUserById;
using Application.Validation;
using Infrastructure.Entities.Users;
using Infrastructure.Foundation;
using Microsoft.EntityFrameworkCore;

namespace Application.Tests.Users.Queries.GetUserById
{
    public class GetUserByIdQueryValidatorTests
    {
        private IAsyncValidator<GetUserByIdQuery> _validator;

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

            _validator = new GetUserByIdQueryValidator( userRepository );
        }

        [Test]
        public async Task ValidationAsync_IdNoFound_Fail()
        {
            // arrange
            GetUserByIdQuery getUserByIdQuery = new GetUserByIdQuery
            {
                Id = 0
            };

            // act
            ValidationResult result = await _validator.ValidationAsync( getUserByIdQuery );

            // assert
            Assert.IsTrue( result.IsFail );
        }

        [Test]
        public async Task ValidationAsync_IdAlreadyExist_Success()
        {
            // arrange
            GetUserByIdQuery getUserByIdQuery = new GetUserByIdQuery
            {
                Id = 1
            };

            // act
            ValidationResult result = await _validator.ValidationAsync( getUserByIdQuery );

            // assert
            Assert.IsFalse( result.IsFail );
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
