using System;
using System.Collections.Generic;
using Alteration.Application.Domain;
using Moq;
using Xunit;

namespace Alteration.Application.Tests.Domain
{
    public sealed class AlterationFormTests
    {
        [Fact]
        public void Create_ShouldReturnNewAlterationFormInstance()
        {
            //arrange
            var clock = new Mock<IClock>();
            var dt = DateTime.UtcNow;
            clock.SetupGet(x => x.UtcNow).Returns(dt);
            var instructions = new List<AlterationInstruction>();

            //act
            var alterationForm = AlterationForm.Create("test", instructions, clock.Object);

            //assert
            Assert.Equal(dt, alterationForm.CreatedOn);
            Assert.Equal((int)AlterationFormStatuses.Created, alterationForm.Status);
            Assert.Equal("test", alterationForm.SuitId);
            Assert.NotEqual(default, alterationForm.Id);
            Assert.Equal(instructions, alterationForm.AlterationInstructions);
        }

        [Fact]
        public void ChangeStatusToPaid_ShouldChangeAlterationFormStatusToPaid()
        {
            //arrange
            var alterationForm = AlterationForm.Create("test", new List<AlterationInstruction>(), SystemClock.Instance);

            //act
            alterationForm.ChangeStatusToPaid();

            //assert
            Assert.Equal((int)AlterationFormStatuses.Paid, alterationForm.Status);
        }

        [Fact]
        public void ChangeStatusToPaid_ShouldNotAffectAnyOtherProperties()
        {
            //arrange
            var clock = new Mock<IClock>();
            var dt = DateTime.UtcNow;
            clock.SetupGet(x => x.UtcNow).Returns(dt);
            var instructions = new List<AlterationInstruction>();

            //act
            var alterationForm = AlterationForm.Create("test", instructions, clock.Object);

            //assert
            Assert.Equal(dt, alterationForm.CreatedOn);
            Assert.Equal((int)AlterationFormStatuses.Created, alterationForm.Status);
            Assert.Equal("test", alterationForm.SuitId);
            Assert.NotEqual(default, alterationForm.Id);
            Assert.Equal(instructions, alterationForm.AlterationInstructions);

            alterationForm.ChangeStatusToPaid();

            Assert.Equal(dt, alterationForm.CreatedOn);
            Assert.Equal((int)AlterationFormStatuses.Paid, alterationForm.Status);
            Assert.Equal("test", alterationForm.SuitId);
            Assert.NotEqual(default, alterationForm.Id);
            Assert.Equal(instructions, alterationForm.AlterationInstructions);
        }

        [Fact]
        public void ChangeStatusToAlterationStarted_ShouldChangeAlterationFormStatusToAlterationStarted()
        {
            //arrange
            var alterationForm = AlterationForm.Create("test", new List<AlterationInstruction>(), SystemClock.Instance);
            alterationForm.ChangeStatusToPaid();

            //act
            alterationForm.ChangeStatusToAlterationStarted();

            //assert
            Assert.Equal((int)AlterationFormStatuses.AlterationStarted, alterationForm.Status);
        }

        [Fact]
        public void ChangeStatusToAlterationStarted_ShouldNotAffectAnyOtherProperties()
        {
            //arrange
            var clock = new Mock<IClock>();
            var dt = DateTime.UtcNow;
            clock.SetupGet(x => x.UtcNow).Returns(dt);
            var instructions = new List<AlterationInstruction>();

            //act
            var alterationForm = AlterationForm.Create("test", instructions, clock.Object);

            //assert
            Assert.Equal(dt, alterationForm.CreatedOn);
            Assert.Equal((int)AlterationFormStatuses.Created, alterationForm.Status);
            Assert.Equal("test", alterationForm.SuitId);
            Assert.NotEqual(default, alterationForm.Id);
            Assert.Equal(instructions, alterationForm.AlterationInstructions);

            alterationForm.ChangeStatusToPaid();
            alterationForm.ChangeStatusToAlterationStarted();

            Assert.Equal(dt, alterationForm.CreatedOn);
            Assert.Equal((int)AlterationFormStatuses.AlterationStarted, alterationForm.Status);
            Assert.Equal("test", alterationForm.SuitId);
            Assert.NotEqual(default, alterationForm.Id);
            Assert.Equal(instructions, alterationForm.AlterationInstructions);
        }

        [Fact]
        public void ChangeStatusToAlterationFinished_ShouldChangeAlterationFormStatusToAlterationFinished()
        {
            //arrange
            var alterationForm = AlterationForm.Create("test", new List<AlterationInstruction>(), SystemClock.Instance);
            alterationForm.ChangeStatusToPaid();
            alterationForm.ChangeStatusToAlterationStarted();

            //act
            alterationForm.ChangeStatusToAlterationFinished();

            //assert
            Assert.Equal((int)AlterationFormStatuses.AlterationFinished, alterationForm.Status);
        }

        [Fact]
        public void ChangeStatusToAlterationFinished_ShouldNotAffectAnyOtherProperties()
        {
            //arrange
            var clock = new Mock<IClock>();
            var dt = DateTime.UtcNow;
            clock.SetupGet(x => x.UtcNow).Returns(dt);
            var instructions = new List<AlterationInstruction>();

            //act
            var alterationForm = AlterationForm.Create("test", instructions, clock.Object);

            //assert
            Assert.Equal(dt, alterationForm.CreatedOn);
            Assert.Equal((int)AlterationFormStatuses.Created, alterationForm.Status);
            Assert.Equal("test", alterationForm.SuitId);
            Assert.NotEqual(default, alterationForm.Id);
            Assert.Equal(instructions, alterationForm.AlterationInstructions);

            alterationForm.ChangeStatusToPaid();
            alterationForm.ChangeStatusToAlterationStarted();
            alterationForm.ChangeStatusToAlterationFinished();

            Assert.Equal(dt, alterationForm.CreatedOn);
            Assert.Equal((int)AlterationFormStatuses.AlterationFinished, alterationForm.Status);
            Assert.Equal("test", alterationForm.SuitId);
            Assert.NotEqual(default, alterationForm.Id);
            Assert.Equal(instructions, alterationForm.AlterationInstructions);
        }
    }
}
