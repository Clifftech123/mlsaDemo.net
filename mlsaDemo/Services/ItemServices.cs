using AutoMapper;
using Microsoft.EntityFrameworkCore;
using mlsaDemo.Context;
using mlsaDemo.DTO;
using mlsaDemo.Interface;
using mlsaDemo.Models;

namespace mlsaDemo.Services
{
    public class ItemServices : IServices
    {
        private readonly AppDbContext _context;
        private readonly ILogger<ItemServices> _logger;
        private readonly IMapper _mapper;


        public ItemServices(AppDbContext context, ILogger<ItemServices> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }


        // Create item
        public async Task<ItemDto> CreateItem(CreateItemDto itemDto)
        {
            var item = _mapper.Map<ItemsModel>(itemDto);
            item.Id = Guid.NewGuid();
            item.CreatedAt = DateTime.UtcNow;
            item.UpdatedAt = DateTime.UtcNow;

            await _context.Items.AddAsync(item);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Item created with ID: {item.Id}");

            return _mapper.Map<ItemDto>(item);
        }


        // Get all items 
        public async Task<IEnumerable<ItemDto>> GetItems()
        {
            var items = await _context.Items.ToListAsync();
            return _mapper.Map<IEnumerable<ItemDto>>(items);
        }


        // Get item by ID
        public async Task<ItemDto> GetItem(Guid id)
        {
            var item = await _context.Items.FirstOrDefaultAsync(x => x.Id == id);
            if (item == null)
            {
                _logger.LogWarning($"Item with ID: {id} not found.");
                throw new KeyNotFoundException($"Item with ID: {id} not found.");
            }
            return _mapper.Map<ItemDto>(item);


        }

        // Update item by ID
        public async Task<ItemDto> UpdateItem(Guid id, UpdateItemDto itemDto)
        {
            var item = await _context.Items.FirstOrDefaultAsync(x => x.Id == id);
            if (item == null)
            {
                _logger.LogWarning($"Item with ID: {id} not found for update.");
                throw new KeyNotFoundException($"Item with ID: {id} not found.");
            }

            item.Name = itemDto.Name;
            item.Description = itemDto.Description;
            item.Price = itemDto.Price;
            item.UpdatedAt = DateTime.UtcNow;
            _context.Items.Update(item);
            await _context.SaveChangesAsync();

            _context.Items.Update(item);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Item with ID: {id} updated.");

            return _mapper.Map<ItemDto>(item);
        }


        // Delete item by ID
        public async Task<ItemDto> DeleteItem(Guid id)
        {
            var item = await _context.Items.FirstOrDefaultAsync(x => x.Id == id);
            if (item == null)
            {
                _logger.LogWarning($"Item with ID: {id} not found for deletion.");
                throw new KeyNotFoundException($"Item with ID: {id} not found.");
            }

            _context.Items.Remove(item);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Item with ID: {id} deleted.");

            return _mapper.Map<ItemDto>(item);
        }
    }
}
