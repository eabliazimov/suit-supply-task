using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Alteration.Application.Domain;
using Alteration.Application.Features.AlterationStarted;
using Alteration.Application.Infrastructure.Exceptions;
using Alteration.Application.Infrastructure.Services;
using Alteration.Application.Infrastructure.Store;
using Moq;
using Moq.EntityFrameworkCore;
using Xunit;

namespace Alteration.Application.Tests.Features.AlterationStarted
{
    public sealed class AlterationStartedCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldChangeStatusToAlterationStarted()
        {
            //arrange
            var dbContext = new Mock<IAlterationApplicationDbContext>();
            var alterationForm = AlterationForm.Create("test", new List<AlterationInstruction>(), SystemClock.Instance);
            var entities = new List<AlterationForm>() { alterationForm };
            dbContext.Setup(x => x.AlterationForms).ReturnsDbSet(entities);
            alterationForm.ChangeStatusToPaid();
            var handler = new AlterationStartedCommandHandler(dbContext.Object);

            //act
            await handler.Handle(new AlterationStartedCommand(alterationForm.Id), CancellationToken.None);

            //assert
            Assert.Equal((int)AlterationFormStatuses.AlterationStarted, alterationForm.Status);
            dbContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_WhenAlterationFormNotFoundInDatabase_ShouldThrowResourceNotFoundException()
        {
            //arrange
            var dbContext = new Mock<IAlterationApplicationDbContext>();
            var entities = new List<AlterationForm>() { };
            dbContext.Setup(x => x.AlterationForms).ReturnsDbSet(entities);
            var handler = new AlterationStartedCommandHandler(dbContext.Object);
            var newId = Guid.NewGuid();

            //act and assert
            var exception = await Assert.ThrowsAsync<ResourceNotFoundException>(() => handler.Handle(new AlterationStartedCommand(newId), CancellationToken.None));

            Assert.Equal($"Alteration Form with id: {newId} could not be find.", exception.Message);
        }

        [Fact]
        public async Task Handle_WhenAlterationIsNotInPaidState_ShouldThrowInvalidOperationException()
        {
            //arrange
            var dbContext = new Mock<IAlterationApplicationDbContext>();
            var alterationForm = AlterationForm.Create("test", new List<AlterationInstruction>(), SystemClock.Instance);
            var entities = new List<AlterationForm>() { alterationForm };
            dbContext.Setup(x => x.AlterationForms).ReturnsDbSet(entities);
            var emailService = new Mock<IEmailNotificationService>();
            var handler = new AlterationStartedCommandHandler(dbContext.Object);

            //act and assert
            var exception = await Assert.ThrowsAsync<InvalidDomainOperationException>(() => handler.Handle(new AlterationStartedCommand(alterationForm.Id), CancellationToken.None));
            Assert.Equal($"Alteration Form is not in {Enum.GetName(AlterationFormStatuses.Paid)} status.", exception.Message);
        }
    }
}
