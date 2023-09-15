using Alteration.Application.Domain;
using Alteration.Application.Features.GetAlterationFormsQuery;
using Alteration.Application.Infrastructure.Store;
using Moq;
using Moq.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Alteration.Application.Tests.Features.GetAlterationFormsQuery
{
    public sealed class GetAlterationFormsQueryHandlerTests
    {
        [Fact]
        public async Task GetAlterationFormsQueryHandlerHandle_ShouldReturnFullResponseModel() 
        {
            //arrange
            var dbContext = new Mock<IAlterationApplicationDbContext>();
            var alterationForms = new List<AlterationForm>
            {
                AlterationForm.Create(
                    "Test",
                    new [] { AlterationInstruction.Create(AlterationTypes.LeftTrousersLeg, 3) },
                    SystemClock.Instance)
            };
            dbContext.SetupGet(x => x.AlterationForms).ReturnsDbSet(alterationForms);

            var handler = new GetAlterationFormsQueryHandler(dbContext.Object);

            //act
            var result = await handler.Handle(new Application.Features.GetAlterationFormsQuery.GetAlterationFormsQuery(), CancellationToken.None);

            //assert
            Assert.NotNull(result);
            Assert.Single(result);
            var form = result.Single();
            Assert.Equal("Test", form.SuitId);
            Assert.Equal((int)AlterationFormStatuses.Created, form.Status);
            Assert.Equal(1, form.AlterationInstructions.Count);
            Assert.Equal(AlterationTypes.LeftTrousersLeg, form.AlterationInstructions.Single().AlterationType);
            Assert.Equal(3d, form.AlterationInstructions.Single().AlterationLength);
        }
    }
}
