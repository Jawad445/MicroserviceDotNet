using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Play.Common.Interfaces;
using Play.Inventory.Dtos;
using Play.Inventory.Entities;
using Play.Inventory.Extentions;

namespace Play.Inventory.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ItemsController : ControllerBase
{
    private readonly IRepository<InventoryItem> _repo;

    public ItemsController(IRepository<InventoryItem> repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<InventoryItemDTO>>> GetAsync(Guid userId)
    {
        if(userId == Guid.Empty)
        {
            return BadRequest();
        }
        var items = (await _repo.GetAllAsync(item=>item.UserId == userId))
                        .Select(item => item.AsDto());

        return Ok(items);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync(GrantItemDTO itemDto)
    {
        var inventoryItem = await _repo.GetAsync(item => item.UserId == itemDto.userId && item.CatalogItemId == itemDto.catelogItemId);
        if(inventoryItem == null)
        {
            InventoryItem newItem = new InventoryItem
            {
                CatalogItemId = itemDto.catelogItemId,
                UserId = itemDto.userId,
                Quantity = itemDto.quantity,
                AcquiredDate = DateTimeOffset.UtcNow
            };

            await _repo.CreateAsync(newItem);
        }
        else
        {
            inventoryItem.Quantity = itemDto.quantity;
            await _repo.UpdateAsync(inventoryItem);
        }

        return Ok();
    }




}

