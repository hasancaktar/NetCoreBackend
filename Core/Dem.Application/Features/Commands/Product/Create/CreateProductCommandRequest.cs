using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dem.Application.Features.Commands.Product.Create;

public class CreateProductCommandRequest :IRequest<CreateProductCommandResponse>
{
    public string Name { get; set; }
}
