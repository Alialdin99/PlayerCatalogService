using System.Data;
using Microsoft.AspNetCore.Mvc;
using PlayCatalog.service.Dtos;
using PlayCatalog.service.Extentions;
using PlayCatalog.service.Repositories;
using PlayCatalog.Services.Entities;

namespace PlayCatalog.service.Controllers{
    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase
    {
        private readonly ItemRepository itemRepository = new ItemRepository();

        [HttpGet]
        public async Task<IEnumerable<ItemDto>> GetItemsAsync()
        {
            var items = (await itemRepository.GetAllAsync())
                        .Select(item => item.AsDto());
            return items;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> GetByIdAsync(Guid id)
        {
            var item = await itemRepository.GetAsync(id);
            if (item == null){return NotFound();}
            return item.AsDto();
        }
        
        [HttpPost]
        public async Task<ActionResult<ItemDto>> PostItemAsync(CreateItemDto createItemDto)
        {
            var item = new Item
            {
                Name = createItemDto.Name,
                Description = createItemDto.Description,
                Price = createItemDto.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };
            await itemRepository.CreateAsync(item);
            return CreatedAtAction(nameof(GetByIdAsync),new{id = item.Id},item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, UpdateItemDto updatedItemDto)
        {
            var existingItem = await itemRepository.GetAsync(id);
            if(existingItem == null){return NotFound();}

            existingItem.Name = updatedItemDto.Name;
            existingItem.Description = updatedItemDto.Description;
            existingItem.Price = updatedItemDto.Price;

            await itemRepository.UpdateAsync(existingItem);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task< ActionResult> Delete(Guid id)
        {
             var item = await itemRepository.GetAsync(id);
            if(item == null){return NotFound();}

            await itemRepository.RemoveAsync(id);
            return NoContent();
        }
    }
}