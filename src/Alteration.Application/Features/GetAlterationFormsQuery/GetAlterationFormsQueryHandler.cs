using Alteration.Application.Domain;
using Alteration.Application.Infrastructure.Store;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Alteration.Application.Features.GetAlterationFormsQuery
{
    public class GetAlterationFormsQueryHandler : IRequestHandler<GetAlterationFormsQuery, List<GetAlterationFormsQueryResponse>>
    {
        private readonly IAlterationApplicationDbContext _alterationApplicationDbContext;

        public GetAlterationFormsQueryHandler(IAlterationApplicationDbContext alterationApplicationDbContext)
        {
            _alterationApplicationDbContext = alterationApplicationDbContext;
        }

        public async Task<List<GetAlterationFormsQueryResponse>> Handle(GetAlterationFormsQuery request, CancellationToken cancellationToken)
        {
            var result = await _alterationApplicationDbContext.AlterationForms.Select(af =>
                new GetAlterationFormsQueryResponse(
                    af.Id,
                    af.Status,
                    af.SuitId,
                    af.CreatedOn)
                {
                    AlterationInstructions =
                            af.AlterationInstructions
                                .Select(ai =>
                                    new GetAlterationFormsQueryResponseAlterationInstruction(
                                        ai.Id,
                                        (AlterationTypes)ai.AlterationTypeId,
                                        ai.AlterationLength)).ToList()
                }).ToListAsync(cancellationToken);

            return result;
        }
    }
}
