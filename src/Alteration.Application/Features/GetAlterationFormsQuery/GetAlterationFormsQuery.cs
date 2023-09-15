using MediatR;

namespace Alteration.Application.Features.GetAlterationFormsQuery
{
    public record class GetAlterationFormsQuery: IRequest<List<GetAlterationFormsQueryResponse>>
    {
    }
}
