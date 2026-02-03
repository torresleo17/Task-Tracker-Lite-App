using Task_Tracker_Lite.Dtos.List;
using Task_Tracker_Lite.Dtos.Task;

namespace Task_Tracker_Lite.Services
{
    public interface ITaskService
    {
        Task<TaskDTO> CreateAsync(int listId, CreateTaskDTO task);
        Task<bool> DeleteAsync(int listId, int taskId);
        Task<IEnumerable<TaskDTO>> GetByListIdAsync(int listId);
        Task<TaskDTO?> UpdateAsync(int listId, int taskId, UpdateTaskDTO task);
    }
}