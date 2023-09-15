﻿using System.Collections.Generic;
using System.Threading;
using System;
using System.Threading.Tasks;
using Alteration.Application.Domain;
using Alteration.Application.Infrastructure.Exceptions;
using Alteration.Application.Infrastructure.Services;
using Alteration.Application.Infrastructure.Store;
using Alteration.Application.Features.PaymentReceived;
using Moq;
using Xunit;
using Moq.EntityFrameworkCore;

namespace Alteration.Application.Tests.Features.PaymentReceived
{
    public sealed class PaymentReceivedCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldChangeStatusToAlterationStarted()
        {
            //arrange
            var dbContext = new Mock<IAlterationApplicationDbContext>();
            var alterationForm = AlterationForm.Create("test", new List<AlterationInstruction>(), SystemClock.Instance);
            var entities = new List<AlterationForm>() { alterationForm };
            dbContext.Setup(x => x.AlterationForms).ReturnsDbSet(entities);
            var handler = new PaymentReceivedCommandHandler(dbContext.Object);

            //act
            await handler.Handle(new PaymentReceivedCommand(alterationForm.Id), CancellationToken.None);

            //assert
            Assert.Equal((int)AlterationFormStatuses.Paid, alterationForm.Status);
            dbContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_WhenAlterationFormNotFoundInDatabase_ShouldThrowResourceNotFoundException()
        {
            //arrange
            var dbContext = new Mock<IAlterationApplicationDbContext>();
            var entities = new List<AlterationForm>() { };
            dbContext.Setup(x => x.AlterationForms).ReturnsDbSet(entities);
            var handler = new PaymentReceivedCommandHandler(dbContext.Object);
            var newId = Guid.NewGuid();

            //act and assert
            var exception = await Assert.ThrowsAsync<ResourceNotFoundException>(() => handler.Handle(new PaymentReceivedCommand(newId), CancellationToken.None));

            Assert.Equal($"Alteration Form with id: {newId} could not be find.", exception.Message);
        }

        [Fact]
        public async Task Handle_WhenAlterationIsNotInPaidState_ShouldThrowInvalidOperationException()
        {
            //arrange
            var dbContext = new Mock<IAlterationApplicationDbContext>();
            var alterationForm = AlterationForm.Create("test", new List<AlterationInstruction>(), SystemClock.Instance);
            var entities = new List<AlterationForm>() { alterationForm };
            alterationForm.ChangeStatusToPaid();
            alterationForm.ChangeStatusToAlterationStarted();
            dbContext.Setup(x => x.AlterationForms).ReturnsDbSet(entities);
            var emailService = new Mock<IEmailNotificationService>();
            var handler = new PaymentReceivedCommandHandler(dbContext.Object);

            //act and assert
            var exception = await Assert.ThrowsAsync<InvalidDomainOperationException>(() => handler.Handle(new PaymentReceivedCommand(alterationForm.Id), CancellationToken.None));
            Assert.Equal($"Alteration Form is not in {Enum.GetName(AlterationFormStatuses.Created)} status.", exception.Message);
        }
    }
}
