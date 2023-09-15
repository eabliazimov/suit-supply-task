using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Alteration.Application.Domain;
using Alteration.Application.Features.AlterationCompleted;
using Alteration.Application.Infrastructure.Exceptions;
using Alteration.Application.Infrastructure.Services;
using Alteration.Application.Infrastructure.Store;
using Moq;
using Moq.EntityFrameworkCore;
using Xunit;

namespace Alteration.Application.Tests.Features.AlterationCompleted
{
    public sealed class AlterationStartedCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldChangeStatusToAlterationFinished()
        {
            //arrange
            var dbContext = new Mock<IAlterationApplicationDbContext>();
            var alterationForm = AlterationForm.Create("test", new List<AlterationInstruction>(), SystemClock.Instance);
            var entities = new List<AlterationForm>() { alterationForm };
            dbContext.Setup(x => x.AlterationForms).ReturnsDbSet(entities);
            alterationForm.ChangeStatusToPaid();
            alterationForm.ChangeStatusToAlterationStarted();
            var emailService = new Mock<IEmailNotificationService>();
            var handler = new AlterationCompletedCommandHandler(dbContext.Object, emailService.Object);

            //act
            await handler.Handle(new AlterationCompletedCommand(alterationForm.Id), CancellationToken.None);

            //assert
            Assert.Equal((int)AlterationFormStatuses.AlterationFinished, alterationForm.Status);
            dbContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_WhenAlterationFormNotFoundInDatabase_ShouldThrowResourceNotFoundException()
        {
            //arrange
            var dbContext = new Mock<IAlterationApplicationDbContext>();
            var entities = new List<AlterationForm>() { };
            dbContext.Setup(x => x.AlterationForms).ReturnsDbSet(entities);
            var emailService = new Mock<IEmailNotificationService>();
            var handler = new AlterationCompletedCommandHandler(dbContext.Object, emailService.Object);
            var newId = Guid.NewGuid();

            //act and assert
            var exception = await Assert.ThrowsAsync<ResourceNotFoundException>(() => handler.Handle(new AlterationCompletedCommand(newId), CancellationToken.None));

            Assert.Equal($"Alteration Form with id: {newId} could not be find.", exception.Message);
        }

        [Fact]
        public async Task Handle_WhenAlterationIsNotInAlterationStartedState_ShouldThrowInvalidOperationException()
        {
            //arrange
            var dbContext = new Mock<IAlterationApplicationDbContext>();
            var alterationForm = AlterationForm.Create("test", new List<AlterationInstruction>(), SystemClock.Instance);
            var entities = new List<AlterationForm>() { alterationForm };
            dbContext.Setup(x => x.AlterationForms).ReturnsDbSet(entities);
            var emailService = new Mock<IEmailNotificationService>();
            var handler = new AlterationCompletedCommandHandler(dbContext.Object, emailService.Object);

            //act and assert
            var exception = await Assert.ThrowsAsync<InvalidDomainOperationException>(() => handler.Handle(new AlterationCompletedCommand(alterationForm.Id), CancellationToken.None));
            Assert.Equal($"Alteration Form is not in {Enum.GetName(AlterationFormStatuses.AlterationStarted)} status.", exception.Message);
        }
    }
}
