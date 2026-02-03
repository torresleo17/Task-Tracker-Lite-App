using Microsoft.AspNetCore.Mvc;
using Task_Tracker_Lite.Dtos.Board;
using Task_Tracker_Lite.Services;

namespace Task_Tracker_Lite.Controllers.Boards
{
    [ApiController]
    [Route("api/[controller]")]
    public class BoardsController : ControllerBase
    {
        private readonly IBoardService _boardService;

        public BoardsController(IBoardService boardService)
        {
            _boardService = boardService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBoard([FromBody] CreatedBoardDTO dto)
        {
            try
            {
                var result = await _boardService.AddBoardAsync(dto);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBoard(int id)
        {
            var result = await _boardService.GetByIdAsync(id);
            if (result is null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBoards()
        {
            try
            {
                var result = await _boardService.GetAllAsync();
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBoard(int id, [FromBody] UpdateBoardDTO dto)
        {
            try
            {
                var result = await _boardService.UpdateAsync(id, dto);
                if (result is null)
                    return NotFound();

                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBoard(int id)
        {
            var result = await _boardService.DeleteAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }

}