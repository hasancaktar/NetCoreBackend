using Dem.Application.Repositories.Product;
using MediatR;

namespace Dem.Application.Features.Commands.Product.Create;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, CreateProductCommandResponse>
{
    private readonly IProductRepository _productRepository;

    public CreateProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<CreateProductCommandResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
    {
        var result = await _productRepository.AddAsync(new()
        {
            Name = request.Name
        });
        CreateProductCommandResponse response = new() { IsSuccess = result };

        if (result)
        {
            await _productRepository.SaveAsync();
            response.Message = "Ürün eklendi";
            return response;
        }
        else
        {
            response.Message = "Ürün ekleme başarısız";
            return response;
        }
    }
}