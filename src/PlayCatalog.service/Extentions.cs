using PlayCatalog.service.Dtos;
using PlayCatalog.Services.Entities;

namespace PlayCatalog.service.Extentions
{
    public static class Extentions
    {
        public static ItemDto AsDto(this Item item)
        {
            return new ItemDto(item.Id, item.Name, item.Description,item.Price,item.CreatedDate);
        }
    }
}