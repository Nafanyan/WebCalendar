using Application.Users.Queries.GetEvents;
using Application.Validation;
using Domain.Entities;
using Domain.Repositories;
using Domain.UnitOfWork;
using Infrastructure.Data.Users;
using Infrastructure.Foundation;
using Microsoft.EntityFrameworkCore;

namespace Application.Tests.Users.Queries.GetEvents
{
    public class GetEventsQueryValidatorTests
    {
        private IAsyncValidator<GetEventsQuery> _validator;

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

            _validator = new GetEventsQueryValidator(userRepository);
        }

        [Test]
        public async Task ValidationAsync_IdAlreadyExist_Success()
        {
            // arrange
            GetEventsQuery getEventsQuery = new GetEventsQuery
            {
                UserId = 1
            };

            // act
            ValidationResult result = await _validator.ValidationAsync(getEventsQuery);

            // assert
            Assert.IsFalse(result.IsFail);
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
