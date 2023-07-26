using Application.UserAuthorizationTokens.Commands.RefreshToken;
using Application.Validation;
using Domain.Entities;
using Domain.Repositories;
using Domain.UnitOfWork;
using Infrastructure.Data.UserAuthorizationTokens;
using Infrastructure.Data.Users;
using Infrastructure.Foundation;
using Microsoft.EntityFrameworkCore;

namespace Application.Tests.UserAuthorizationTokens.Commands.RefreshToken
{
    public class RefreshTokenCommandValidatorTests
    {
        private IAsyncValidator<RefreshTokenCommand> _validator;

        [SetUp]
        public async Task Setup()
        {
            string dbName = $"EventDb_{DateTime.Now.ToFileTimeUtc()}";
            DbContextOptions<WebCalendarDbContext> dbContextOptions = new DbContextOptionsBuilder<WebCalendarDbContext>()
                .UseInMemoryDatabase(dbName)
                .Options;
            WebCalendarDbContext webCalendarDbContext = new WebCalendarDbContext(dbContextOptions);

            IUserAuthorizationTokenRepository userAuthorizationTokenRepository = new UserAuthorizationRepository(webCalendarDbContext);
            await InitData(userAuthorizationTokenRepository, webCalendarDbContext);

            _validator = new RefreshTokenCommandValidator(userAuthorizationTokenRepository);
        }

        [Test]
        public async Task ValidationAsync_TokenIsNull_Fail()
        {
            // arrange
            RefreshTokenCommand refreshTokenCommand = new RefreshTokenCommand
            {
                RefreshToken = null
            };

            // act
            ValidationResult result = await _validator.ValidationAsync(refreshTokenCommand);

            // assert
            Assert.IsTrue(result.IsFail);
        }

        [Test]
        public async Task ValidationAsync_TokenNotExpired_Success()
        {
            // arrange
            RefreshTokenCommand refreshTokenCommand = new RefreshTokenCommand
            {
                RefreshToken = "RefreshToken"
            };

            // act
            ValidationResult result = await _validator.ValidationAsync(refreshTokenCommand);

            // assert
            Assert.IsFalse(result.IsFail);
        }

        private async Task InitData(
            IUserAuthorizationTokenRepository userAuthorizationTokenRepository, 
            WebCalendarDbContext webCalendarDbContext)
        {
            IUserRepository userRepository = new UserRepository(webCalendarDbContext);
            User user = new User("login", "passwordHash");
            userRepository.Add(user);

            UserAuthorizationToken userAuthorizationToken = new UserAuthorizationToken(
                1,
                "RefreshToken",
                DateTime.Now.AddMinutes(1));
            userAuthorizationTokenRepository.Add(userAuthorizationToken);

            IUnitOfWork unitOfWork = new UnitOfWork(webCalendarDbContext);
            await unitOfWork.CommitAsync();
        }
    }
}
