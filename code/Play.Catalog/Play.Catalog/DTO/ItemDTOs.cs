using System.ComponentModel.DataAnnotations;

namespace Play.Catalog.DTO;

public record ItemDTO(Guid Id,string Name, string Description, decimal Price, DateTimeOffset CreatedOn);
public record CreateItemDTO([Required]string Name, string Description, [Range(1,1000)]decimal Price);
public record UpdateItemDTO([Required] string Name, string Description, [Range(1, 1000)] decimal Price);
