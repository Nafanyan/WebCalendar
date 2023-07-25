using Application.Events.Queries.GetEvent;
using Application.Validation;
using Domain.Entities;
using Domain.Repositories;
using Domain.UnitOfWork;
using Infrastructure.Data.Events;
using Infrastructure.Data.Users;
using Infrastructure.Foundation;
using Microsoft.EntityFrameworkCore;

namespace Application.Tests.Events.Queries.GetEvent
{
    public class GetEventQueryValidatorTests
    {
        private IAsyncValidator<GetEventQuery> _validator;

        [SetUp]
        public async Task Setup()
        {
            string dbName = $"EventDb_{DateTime.Now.ToFileTimeUtc()}";
            DbContextOptions<WebCalendarDbContext> dbContextOptions = new DbContextOptionsBuilder<WebCalendarDbContext>()
                .UseInMemoryDatabase(dbName)
                .Options;
            WebCalendarDbContext webCalendarDbContext = new WebCalendarDbContext(dbContextOptions);

            IEventRepository eventRepository = new EventRepository(webCalendarDbContext);
            await InitData(eventRepository, webCalendarDbContext);

            _validator = new GetEventQueryValidator(eventRepository);
        }

        [Test]
        public async Task ValidationAsync_StartDateAndsEndDateNotSameDay_Fail()
        {
            // arrange
            GetEventQuery query = new GetEventQuery
            {
                UserId = 0,
                StartEvent = new DateTime(2023, 11, 1),
                EndEvent = new DateTime(2023, 11, 2)
            };

            // act
            ValidationResult result = await _validator.ValidationAsync(query);

            // assert
            Assert.IsTrue(result.IsFail);
        }

        [Test]
        public async Task ValidationAsync_StartDateLaterThenEndDate_Fail()
        {
            // arrange
            GetEventQuery query = new GetEventQuery
            {
                UserId = 0,
                StartEvent = new DateTime(1001),
                EndEvent = new DateTime(1000)
            };

            // act
            ValidationResult result = await _validator.ValidationAsync(query);

            // assert
            Assert.IsTrue(result.IsFail);
        }

        [Test]
        public async Task ValidationAsync_StartDateIncludedInExistEvent_Success()
        {
            // arrange
            GetEventQuery query = new GetEventQuery
            {
                UserId = 1,
                StartEvent = new DateTime(1500),
                EndEvent = new DateTime(2500)
            };

            // act
            ValidationResult result = await _validator.ValidationAsync(query);

            // assert
            Assert.IsFalse(result.IsFail);
        }

        [Test]
        public async Task ValidationAsync_EndDateIncludedInExistEvent_Success()
        {
            // arrange
            GetEventQuery query = new GetEventQuery
            {
                UserId = 1,
                StartEvent = new DateTime(500),
                EndEvent = new DateTime(1500)
            };

            // act
            ValidationResult result = await _validator.ValidationAsync(query);

            // assert
            Assert.IsFalse(result.IsFail);
        }
        private async Task InitData(IEventRepository eventRepository, WebCalendarDbContext webCalendarDbContext)
        {
            IUserRepository userRepository = new UserRepository(webCalendarDbContext);
            User user = new User("login", "passwordHash");
            userRepository.Add(user);

            Event newEvent = new Event(1, "name", "", new DateTime(1000), new DateTime(2000));
            eventRepository.Add(newEvent);

            IUnitOfWork unitOfWork = new UnitOfWork(webCalendarDbContext);
            await unitOfWork.CommitAsync();
        }
    }
}
