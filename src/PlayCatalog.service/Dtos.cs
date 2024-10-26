using System;
using System.ComponentModel.DataAnnotations;
namespace PlayCatalog.service.Dtos
{
    public record ItemDto(Guid Id, string Name, string Description, Decimal Price, DateTimeOffset CreatedDate);
    public record CreateItemDto([Required]string Name, [Required]string Description, Decimal Price);
    public record UpdateItemDto([Required]string Name, [Required]string Description, Decimal Price);
}