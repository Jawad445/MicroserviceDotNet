namespace Play.Inventory.Dtos;

public record GrantItemDTO(Guid userId, Guid catelogItemId, int quantity);
public record InventoryItemDTO(Guid catelogItemId, int quantity, DateTimeOffset acquiredDate);

