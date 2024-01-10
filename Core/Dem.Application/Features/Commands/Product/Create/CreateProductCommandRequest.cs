using MediatR;

namespace Dem.Application.Features.Commands.Product.Create;

public class CreateProductCommandRequest : IRequest<CreateProductCommandResponse>
{
    public string Name { get; set; }
}