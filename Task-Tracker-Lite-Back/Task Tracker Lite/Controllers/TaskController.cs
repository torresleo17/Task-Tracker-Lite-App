using Microsoft.AspNetCore.Mvc;
using Task_Tracker_Lite.Dtos.List;
using Task_Tracker_Lite.Services;

namespace Task_Tracker_Lite.Controllers.Tasks
{
    [ApiController]
    [Route("api/boards/{boardId}/lists/{listId}/tasks")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask(int listId, [FromBody] CreateTaskDTO dto)
        {
            try
            {
                var result = await _taskService.CreateAsync(listId, dto);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetTasksByList(int listId)
        {
            try
            {
                var result = await _taskService.GetByListIdAsync(listId);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{taskId}")]
        public async Task<IActionResult> UpdateTask(int listId, int taskId, [FromBody] UpdateTaskDTO dto)
        {
            try
            {
                var result = await _taskService.UpdateAsync(listId, taskId, dto);
                if (result is null)
                    return NotFound();

                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{taskId}")]
        public async Task<IActionResult> DeleteTask(int listId, int taskId)
        {
            var result = await _taskService.DeleteAsync(listId, taskId);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}