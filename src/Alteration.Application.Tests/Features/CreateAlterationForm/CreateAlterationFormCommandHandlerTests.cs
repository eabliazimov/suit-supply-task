using Alteration.Application.Domain;
using Alteration.Application.Infrastructure.Store;
using Moq;
using System.Threading.Tasks;
using System.Threading;
using Xunit;
using System.Linq;
using Alteration.Application.Features.CreateAlterationForm;
using Moq.EntityFrameworkCore;
using System.Collections.Generic;

namespace Alteration.Application.Tests.Features.CreateAlterationForm
{
    public sealed class CreateAlterationFormCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldCreateNewAlterationFormInDatabase()
        {
            //arrange
            var dbContext = new Mock<IAlterationApplicationDbContext>();
            var alterationForms = new List<AlterationForm>();
            dbContext.SetupGet(x => x.AlterationForms).ReturnsDbSet(alterationForms);
            dbContext.Setup(d => d.AlterationForms.Add(It.IsAny<AlterationForm>())).Callback<AlterationForm>((s) => alterationForms.Add(s));

            var handler = new CreateAlterationFormCommandHandler(dbContext.Object, new Mock<IClock>().Object);

            //act
            await handler.Handle(new CreateAlterationFormCommand()
            {
                SuitReference = "Test",
                AlterationInstructions =
                    new CreateAlterationFormAlterationInstruction[]
                    {
                        new CreateAlterationFormAlterationInstruction(AlterationTypes.LeftJacketSleeve, 3)
                    }
            }, CancellationToken.None);

            //assert
            dbContext.Verify(x => x.AlterationForms.Add(It.IsAny<AlterationForm>()), Times.Once);
            Assert.NotEmpty(alterationForms);
            var alterationForm = alterationForms.Single();
            Assert.Equal((int)AlterationFormStatuses.Created, alterationForm.Status);
            Assert.Equal("Test", alterationForm.SuitId);
            Assert.NotEqual(default, alterationForm.Id);
            Assert.Equal(1, alterationForm.AlterationInstructions.Count);
            Assert.Equal((int)AlterationTypes.LeftJacketSleeve, alterationForm.AlterationInstructions.Single().AlterationTypeId);
            Assert.Equal(3d, alterationForm.AlterationInstructions.Single().AlterationLength);
            dbContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
