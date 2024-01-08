using Dem.Domain.Entities.Common;

namespace Dem.Domain.Entities;

public class Product:BaseEntity
{
    public string Name { get; set; } = string.Empty;
}

