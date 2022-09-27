using Play.Catalog.DTO;
using Play.Catalog.Entities;

namespace Play.Catalog.Extentions
{
    public static class Extention
    {
        public static ItemDTO asDto(this Item item)
        {
            return new ItemDTO(item.Id, item.Name, item.Description, item.Price, item.CreatedOn);
        } 

    }
}
