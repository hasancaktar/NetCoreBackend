using Dem.Application.CustomAttributes;
using Dem.Application.Repositories.Product;
using MapsterMapper;
using MediatR;

namespace Dem.Application.Features.Queries.Product.GetAll;

public class ProductGetAllQueryHandler : IRequestHandler<ProductGetAllQueryRequest, List<ProductGetAllQueryResponse>>
{
    private IProductRepository _productRepository;
    private IMapper _mapper;

    public ProductGetAllQueryHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    [Logging("LOGLAMA")]
    public async Task<List<ProductGetAllQueryResponse>> Handle(ProductGetAllQueryRequest request, CancellationToken cancellationToken)
    {
        var productList = _productRepository.GetAll();
        var result = _mapper.Map<List<ProductGetAllQueryResponse>>(productList);
        return result;
    }
}