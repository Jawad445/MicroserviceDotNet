using Play.Inventory.Dtos;
using Play.Inventory.Entities;

namespace Play.Inventory.Extentions;

public static class Extentions
{
    public static InventoryItemDTO AsDto(this InventoryItem item)
    {
        return new InventoryItemDTO(item.CatalogItemId, item.Quantity, item.AcquiredDate);
    }
}
