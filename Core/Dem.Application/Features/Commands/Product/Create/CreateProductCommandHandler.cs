using Dem.Application.Repositories.Product;
using MediatR;
using Dem.Application.Abstraction;

namespace Dem.Application.Features.Commands.Product.Create;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, CreateProductCommandResponse>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
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
            await _productRepository.SaveChangesAsync();
            await _unitOfWork.CommitAsync();

            response.Message = "Ürün eklendi";
            return response;
        }
        throw new ArgumentException("Ürün ekleme başarısız");
    }
}