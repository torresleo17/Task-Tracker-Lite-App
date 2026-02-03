using Task_Tracker_Lite.Domain;

namespace Task_Tracker_Lite.Repository
{
    public interface ITaskItemRepository
    {
        public Task<TaskItem> CreateAsync(TaskItem taskItem);
        public Task<bool> DeleteAsync(int id);
        public Task<IEnumerable<TaskItem>> GetAllAsync();
        public Task<TaskItem?> GetByIdAsync(int id);
        public Task<IEnumerable<TaskItem>> GetAllTasksByListIdAsync(int listId);
        public Task<TaskItem?> UpdateAsync(TaskItem taskItem);
        public Task<bool> DeleteAllByListIdAsync(int listId);
    }
}