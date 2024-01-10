using MediatR;

namespace Dem.Application.Features.Queries.Product.GetAll;

public class ProductGetAllQueryRequest : IRequest<List<ProductGetAllQueryResponse>>
{
}