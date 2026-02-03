using Microsoft.AspNetCore.Mvc;
using Task_Tracker_Lite.Dtos.List;
using Task_Tracker_Lite.Services;

namespace Task_Tracker_Lite.Controllers.Lists
{
    [ApiController]
    [Route("api/boards/{boardId}/lists")]
    public class ListsController : ControllerBase
    {
        private readonly IListService _listService;

        public ListsController(IListService listService)
        {
            _listService = listService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateList(int boardId, [FromBody] CreateListDTO dto)
        {
            try
            {
                var result = await _listService.CreateAsync(boardId, dto);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpGet]
        public async Task<IActionResult> GetAllListsByBoard(int boardId)
        {
            try
            {
                var result = await _listService.GetByBoardIdAsync(boardId);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{listId}")]
        public async Task<IActionResult> UpdateList(int boardId, int listId, [FromBody] UpdateListDTO dto)
        {
            try
            {
                var result = await _listService.UpdateAsync(boardId, listId, dto);
                if (result is null)
                    return NotFound();

                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{listId}")]
        public async Task<IActionResult> DeleteList(int boardId, int listId)
        {
            var result = await _listService.DeleteAsync(boardId, listId);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }

}