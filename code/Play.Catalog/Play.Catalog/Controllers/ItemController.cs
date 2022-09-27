﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Play.Catalog.DTO;
using Play.Catalog.Entities;
using Play.Catalog.Extentions;
using Play.Catalog.Repositories;

namespace Play.Catalog.Controllers
{
    [ApiController]
    [Route("api/Item")]
    public class ItemController : ControllerBase
    {
        private readonly ItemRepository repo = new();        

        [HttpGet("GetAsync")]
        public async Task<IEnumerable<ItemDTO>> GetAsync()
        {
            var items = (await repo.GetAllAsync()).Select(item => item.asDto());
            return items;
        }
        
        [HttpGet("GetByIdAsync/{Id}")]
        public async Task<ActionResult<ItemDTO>> GetByIdAsync(Guid Id)
        {
            var item = await repo.GetAsync(Id);
            if(item == null)
            {
                return NotFound();
            }
            return Ok(item.asDto());
        }

        [HttpPost("PostAsync")]
        public async Task<ActionResult<ItemDTO>> PostAsync(CreateItemDTO item)
        {
            var Newitem = new Item()
            {
                Name = item.Name,
                Description = item.Description,
                Price = item.Price,
                CreatedOn = DateTimeOffset.UtcNow
            };
            //var newitem = new ItemDTO(Guid.NewGuid(), item.Name, item.Description, item.Price, DateTimeOffset.UtcNow);
            //items.Add(newitem);
            await repo.CreateAsync(Newitem);
            return CreatedAtAction(nameof(GetByIdAsync), new { Id = Newitem.Id});
        }

        [HttpPut("PutAsync/{Id}")]
        public async Task<IActionResult> PutAsync(Guid Id, UpdateItemDTO item)
        {
            var existingItem = await repo.GetAsync(Id);// items.Where(x => x.Id.Equals(Id)).SingleOrDefault();
            if(existingItem  == null)
            {
                return NotFound();
            }
            existingItem.Name = item.Name;
            existingItem.Description = item.Description;
            existingItem.Price = item.Price;

            await repo.UpdateAsync(existingItem);
            return CreatedAtAction(nameof(GetByIdAsync), new { Id = existingItem.Id });
        }

        [HttpDelete("DeleteAsync/{Id}")]
        public async Task<IActionResult> DeleteAsync(Guid Id)
        {
            var existingItem = await repo.GetAsync(Id);// items.Where(x => x.Id.Equals(Id)).SingleOrDefault();
            if (existingItem == null)
            {
                return NotFound();
            }

            await repo.RemoveAsync(existingItem.Id);
            return NoContent();
        }

    }
}
