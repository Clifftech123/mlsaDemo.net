using mlsaDemo.DTO;

namespace mlsaDemo.Interface
{
    public interface IServices
    {
        Task<IEnumerable<ItemDto>> GetItems();
        Task<ItemDto> GetItem(Guid id);
        Task<ItemDto> CreateItem(CreateItemDto itemDto);
        Task<ItemDto> UpdateItem(Guid id, UpdateItemDto itemDto);
        Task<ItemDto> DeleteItem(Guid id);
    }
}
