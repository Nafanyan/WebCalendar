﻿using Application.Events.Commands.CreateEvent;
using Application.Events.Commands.DeleteEvent;
using Application.Validation;
using Domain.Entities;
using Domain.Repositories;
using Domain.UnitOfWork;
using Infrastructure.Data.Events;
using Infrastructure.Data.Users;
using Infrastructure.Foundation;
using Microsoft.EntityFrameworkCore;

namespace Application.Tests.Events.Commands.DeleteEvent
{
    public class DeleteEventCommandValidatorTests
    {
        private IAsyncValidator<DeleteEventCommand> _validator;

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

            _validator = new DeleteEventCommandValidator(eventRepository);
        }

        [Test]
        public async Task ValidationAsync_StartDateAndsEndDateNotSameDay_Fail()
        {
            // arrange
            DeleteEventCommand command = new DeleteEventCommand
            {
                StartEvent = new DateTime(2023, 11, 1),
                EndEvent = new DateTime(2023, 11, 2)
            };

            // act
            ValidationResult result = await _validator.ValidationAsync(command);

            // assert
            Assert.IsTrue(result.IsFail);
        }

        [Test]
        public async Task ValidationAsync_StartDateLaterThenEndDate_Fail()
        {
            // arrange
            DeleteEventCommand command = new DeleteEventCommand
            {
                StartEvent = new DateTime(1001),
                EndEvent = new DateTime(1000)
            };

            // act
            ValidationResult result = await _validator.ValidationAsync(command);

            // assert
            Assert.IsTrue(result.IsFail);
        }

        [Test]
        public async Task ValidationAsync_StartDateIncludedInExistEvent_Fail()
        {
            // arrange
            DeleteEventCommand command = new DeleteEventCommand
            {
                UserId = 1,
                StartEvent = new DateTime(1500),
                EndEvent = new DateTime(2500)
            };

            // act
            ValidationResult result = await _validator.ValidationAsync(command);

            // assert
            Assert.IsTrue(result.IsFail);
        }

        [Test]
        public async Task ValidationAsync_EndDateIncludedInExistEvent_Fail()
        {
            // arrange
            DeleteEventCommand command = new DeleteEventCommand
            {
                UserId = 1,
                StartEvent = new DateTime(500),
                EndEvent = new DateTime(1500)
            };

            // act
            ValidationResult result = await _validator.ValidationAsync(command);

            // assert
            Assert.IsTrue(result.IsFail);
        }

        [Test]
        public async Task ValidationAsync_NewEventIncludedExistEvent_Fail()
        {
            // arrange
            DeleteEventCommand command = new DeleteEventCommand
            {
                UserId = 1,
                StartEvent = new DateTime(100),
                EndEvent = new DateTime(2500)
            };

            // act
            ValidationResult result = await _validator.ValidationAsync(command);

            // assert
            Assert.IsTrue(result.IsFail);
        }

        [Test]
        public async Task ValidationAsync_ExistEventIncludedNewEvent_Fail()
        {
            // arrange
            DeleteEventCommand command = new DeleteEventCommand
            {
                UserId = 1,
                StartEvent = new DateTime(1100),
                EndEvent = new DateTime(1500)
            };

            // act
            ValidationResult result = await _validator.ValidationAsync(command);

            // assert
            Assert.IsTrue(result.IsFail);
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
