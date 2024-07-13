using Microsoft.AspNetCore.Mvc;
using mlsaDemo.DTO;
using mlsaDemo.Interface;

namespace mlsaDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServicesController : ControllerBase
    {

        private IServices _services;    


        public ServicesController(IServices services)
        {
            _services = services;
        }


        // GET: api/Services
        [HttpGet]
        public async Task<IActionResult> GetItems()
        {
            try
            {
                var items = await _services.GetItems();
                if (items == null || !items.Any())
                {
                    return Ok(new { message = "No items found" });
                }
                return Ok(new { message = "Successfully retrieved all items", data = items });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving all items", error = e.Message });
            }
        }

        // GET: api/Services/{id}


        [HttpGet("{id}")]
        public async Task<IActionResult> GetItem(Guid id)
        {
            try
            {
                var item = await _services.GetItem(id);
                return Ok(new { message = "Successfully retrieved item", data = item });
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(new { message = e.Message });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving item", error = e.Message });
            }
        }



        // POST: api/Services

        [HttpPost]
        public async Task<IActionResult> CreateItem([FromBody] CreateItemDto itemDto)
        {

            // Validate the model

            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid model state", data = ModelState });
            }

            try
            {
                var item = await _services.CreateItem(itemDto);
                return Ok(new { message = "item is  successfully created" });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = "An error occurred while creating item", error = e.Message });
            }
        }


        // PUT: api/Services/id 

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(Guid id, [FromBody] UpdateItemDto itemDto)
        {
            // Validate the model
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid model state", data = ModelState });
            }

            try
            {
                var item = await _services.UpdateItem(id, itemDto);
                if (item == null)
                {
                    return NotFound(new { message = $"Item not found with {id}" });
                }
                // Removed the redundant call to UpdateItem
                return Ok(new { message = "Item is successfully updated" });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = "An error occurred while updating item", error = e.Message });
            }
        }

        // DELETE: api/Services/id

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(Guid id)
        {
            try
            {
                var item = await _services.DeleteItem(id);
                return Ok(new { message = $"Item is successfully deleted with {id}" });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = "An error occurred while deleting item", error = e.Message });
            }
        }



    }
}
